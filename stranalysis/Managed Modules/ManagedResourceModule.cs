using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using stranalysis.Core;

namespace stranalysis.Managed_Modules
{
    /// <summary>
    /// The ManagedResourceModule detects the existence of a resource reader, paddings, and encrypted resources inside the assembly
    /// </summary>
    public class ManagedResourceModule : Module
    {
        public ManagedResourceModule() : base("Managed Resources") { }
        /// <summary>
        /// Runs the module
        /// </summary>
        public override void Run(List<string> strings) {
            base.Run(strings);

            // Module functionality
            string[] paddingStrings = {"PADPAD", "PADDING", "PADX", "PADDINGX"};
            string[] confuserCrypterStrings = {"Confuser", "Crypter", "confuser", "crypter", "Protector", "protector"};
            string[] resourceReadingStrings = {"System.Resources.ResourceReader", "GetManifestResourceStream", "GetResourceString"};
            bool hasResourceReader = false;
            bool hasPadding = false;
            bool hasEncryptedData = false;
            bool hasConfuserOrCrypter = false;
            int encDataLength = 0;

            for (int i = 0; i < strings.Count; i++) {

                if (resourceReadingStrings.Any(p => strings[i].Contains(p)) && !hasResourceReader) {
                    Program.WriteLine("* Reads embedded resources");
                    hasResourceReader = true;
                }

                if (!hasPadding && paddingStrings.Any(p => strings[i].Contains(p))) {
                    Program.WriteLine("* Contains padding");
                    hasPadding = true;
                }

                if (hasResourceReader && !hasEncryptedData && strings[i].Length >= 256) {
                    if (ShannonEntropy(strings[i]) > 5.2) {
                        encDataLength += strings[i].Length;
                    }
                }

                if (!hasEncryptedData && encDataLength > 4096) {
                    hasEncryptedData = true;
                    Program.WriteLine("* Contains encrypted resources");
                }

                if (!hasConfuserOrCrypter && confuserCrypterStrings.Any(p => strings[i].Contains(p))) {
                    hasConfuserOrCrypter = true;
                    Program.WriteLine("* Has confuser/crypter references");
                }

                if(strings[i].Contains("level=\"requireAdministrator\""))
                    Program.WriteLine("* Requires administrator rights");
            }
        }

        /// <summary>
        /// Returns bits of entropy represented in a given string, per 
        /// </summary>
        public static double ShannonEntropy(string s)
        {
            var map = new Dictionary<char, int>();
            foreach (char c in s)
            {
                if (!map.ContainsKey(c))
                    map.Add(c, 1);
                else
                    map[c] += 1;
            }

            double result = 0.0;
            int len = s.Length;
            foreach (var item in map)
            {
                var frequency = (double)item.Value / len;
                result -= frequency * (Math.Log(frequency) / Math.Log(2));
            }

            return result;
        }
    }


}
