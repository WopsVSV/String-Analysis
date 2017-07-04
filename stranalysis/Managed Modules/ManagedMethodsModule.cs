using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using stranalysis.Core;

namespace stranalysis.Managed_Modules
{
    /// <summary>
    /// The DotNetMethodsModule evaluates the methods found inside the strings and displays their general usage
    /// </summary>
    public class ManagedMethodsModule : Module
    {
        public ManagedMethodsModule() : base ("Methods") { }

        /// <summary>
        /// Runs the module
        /// </summary>
        public override void Run(List<string> strings) {
            base.Run(strings);

            var methods = new List<Method>();

            // Read methods from resources
            foreach (var line in Properties.Resources.ManagedMethods.Split('\n')) {
                if (string.IsNullOrEmpty(line.Replace("\r",string.Empty)) || line.StartsWith(@"//")) continue;
                methods.Add(Method.ParseMethod(line.Replace("\r",string.Empty)));
            }

            // Evaluate strings
            int j;
            for (int i = 0; i < strings.Count; i++) {
                for(j = 0; j < methods.Count; j++)
                    if (methods[j].Check(strings[i])) {
                        Program.WriteLine(methods[j].Interpretation);
                    }
            }
        }


        private class Method
        {
            private static List<string> usedInterps = new List<string>();

            public DefString[] DefiningStrings { get; set; }
            public string Interpretation { get; set; }
            public bool Shown { get; set; } = false;

            public Method(DefString[] defStrings, string interp) {
                DefiningStrings = defStrings;
                Interpretation = interp;
            }

            public bool Check(string value) 
            {
                if (usedInterps.Any(p => p == Interpretation))
                    return false;

                for(int i = 0; i < DefiningStrings.Length; i++)
                    if (DefiningStrings[i].Definition == value)
                        DefiningStrings[i].Seen = true;

                if (DefiningStrings.All(p => p.Seen))
                {
                    usedInterps.Add(Interpretation);
                    return true;
                }

                return false;
            }

            private static DefString[] defStrings;
            public static Method ParseMethod(string rawdata) {
                int plusCount = rawdata.Count(p => p == '+');

                defStrings = new DefString[plusCount + 1];

                if (rawdata.Contains("+"))
                {
                    var arr = rawdata.Split('|')[0].Split('+');
                    for (int i = 0; i < arr.Length; i++)
                    {
                        defStrings[i] = new DefString(arr[i]);
                    }
                }
                else
                    defStrings[0] = new DefString(rawdata.Split('|')[0]);

                return new Method(defStrings, rawdata.Split('|')[1]);
            }

            public struct DefString
            {
                public string Definition;
                public bool Seen;
                public DefString(string def) { Definition = def; Seen = false;  }
            }
        }
    }
}
