using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using stranalysis.Core;

namespace stranalysis.Managed_Modules
{
    public class PInvokeModule : Module
    {
        public PInvokeModule() : base ("P/Invoke Libraries") { }

        /// <summary>
        /// Runs the module
        /// </summary>
        /// <param name="strings"></param>
        public override void Run(List<string> strings) {
            base.Run(strings);

            // Load from file
            var libraries = new List<string>();

            // Read library names from resources
            foreach (var line in Properties.Resources.PInvokeLibraries.Split('\n'))
            {
                // Check for invalid lines
                if (string.IsNullOrEmpty(line.Replace("\r", string.Empty)) || line.StartsWith("//"))
                    continue;

                libraries.Add(line.Replace("\r",string.Empty));
            }

            for (int i = 0; i < strings.Count; i++) {
                for (var li = 0; li < libraries.Count; li++) {
                    string lib = libraries[li];
                    if (string.Equals(lib, strings[i], StringComparison.InvariantCultureIgnoreCase) || string.Equals(lib.Split('.')[0], strings[i],
                            StringComparison.InvariantCultureIgnoreCase)) {
                        Program.WriteLine($"Imported: {lib}");
                        libraries.RemoveAt(li);
                    }
                }
            }
        }
    }
}
