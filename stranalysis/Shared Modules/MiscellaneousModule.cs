using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using stranalysis.Core;

namespace stranalysis.Shared_Modules
{
    /// <summary>
    /// The MiscellaneousModule gives miscellaneous information
    /// IP ADDRESS, MAILS
    /// </summary>
    public class MiscellaneousModule : Module
    {
        public MiscellaneousModule() : base ("Miscellaneous") { }

        /// <summary>
        /// Runs the module
        /// </summary>
        private readonly string[] mailProviders = {"@gmail.com", "@yahoo.com", "@mail.com", "@outlook.com", "@inbox.com"};
        public override void Run(List<string> strings) {
            base.Run(strings);

            for (int i = 0; i < strings.Count; i++) {

                // Get mail addresses
                if (strings[i].Contains("@") && strings[i].Contains(".")) {
                    foreach (var provider in mailProviders) {
                        if (strings[i].EndsWith(provider) && strings[i].Length < 32) {
                            Program.WriteLine($"Mail: {strings[i]}");
                        }
                        else if (strings[i].Contains(provider) && strings[i].Length >= 32) {
                            Regex fileRegex = new Regex(@"([\w]+PROVIDER)".Replace("PROVIDER", provider));
                            string val = fileRegex.Match(strings[i]).Value;
                            if (!string.IsNullOrEmpty(val) && val.Length != provider.Length && val.Length < 32) {
                                Program.WriteLine($"Mail: {val}");
                            }
                        }
                    }
                }

                // Get IP addresses
                if (strings[i].Contains(".") && CheckIpValid(strings[i])) {
                    Program.WriteLine($"IP Address: {strings[i]}");
                }
                    
            }
            
        }

        /// <summary>
        /// Checks if the string is a valid IP
        /// </summary>
        private string allowedCharsIP = "0123456789.";
        public bool CheckIpValid(string strIP) {

            if (strIP == "0.0.0.0" || strIP == "1.0.0.0" || strIP == "1.0.0.1")
                return false;

            if(strIP.Any(p => !allowedCharsIP.Contains(p)))
                return false;

            //  Split string by ".", check that array length is 3
            char chrFullStop = '.';
            string[] arrOctets = strIP.Split(chrFullStop);
            if (arrOctets.Length != 4)
            {
                return false;
            }
            //  Check each substring checking that the int value is less than 255 and that is char[] length is !> 2
            Int16 MAXVALUE = 255;
            Int32 temp; // Parse returns Int32
            int nrinvalidoctets = 0;
            int nrnulloctets = 0;
            foreach (String strOctet in arrOctets)
            {
                if (strOctet.Length > 3)
                {
                    return false;
                }

                temp = int.Parse(strOctet);
                if (temp > MAXVALUE)
                {
                    return false;
                }
                if (temp < 8)
                    nrinvalidoctets++;
                if (temp == 0)
                    nrnulloctets++;
            }

            if (nrinvalidoctets == 4 || nrnulloctets >= 3)
                return false;

            return true;
        }
    }
}
