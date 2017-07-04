using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using stranalysis.Core;

namespace stranalysis.Shared_Modules
{
    /// <summary>
    /// Detects blacklisted keywords
    /// </summary>
    public class BlacklistModule : Module
    {
        public BlacklistModule() : base ("Blacklist") { }

        /// <summary>
        /// Runs the module
        /// </summary>
        public override void Run(List<string> strings) {
            base.Run(strings);

            var blacklist = new List<Definition>();
            var alreadyUsedInterps = new List<string>();

            // Read list from resources
            foreach (var line in Properties.Resources.Blacklist.Split('\n'))
            {
                if (string.IsNullOrEmpty(line.Replace("\r", string.Empty)) || line.StartsWith(@"//")) continue;
                blacklist.Add(Definition.ParseDefinition(line.Replace("\r", string.Empty)));
            }

            // Check
            for (int i = 0; i < strings.Count; i++) {
                foreach (var def in blacklist) {
                    if (strings[i].Contains(def.DefiningString) && alreadyUsedInterps.All(p => p != def.Interpretation)) {
                        Program.WriteLine(def.Interpretation);
                        alreadyUsedInterps.Add(def.Interpretation);
                    }
                }
            }
        }

        private class Definition
        {
            public string DefiningString { get; set; }
            public string Interpretation { get; set; }

            public Definition(string defString, string interp)
            {
                DefiningString = defString;
                Interpretation = interp;
            }

            public static Definition ParseDefinition(string rawdata)
            {
                return new Definition(rawdata.Split('|')[0], rawdata.Split('|')[1]);
            }
        }
    }
}
