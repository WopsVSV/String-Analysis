using System.Collections.Generic;
using System.Linq;
using stranalysis.Core;

namespace stranalysis.Managed_Modules
{
    public partial class ManagedMethodsModule : Module
    {
        /// <summary>
        /// A method is composed of defining strings and a interpretation
        /// It is the format used in the ManagedMethods.txt file
        /// </summary>
        private class Method
        {
            /// <summary>
            /// A static list of used interpretations to avoid redundancy of displays
            /// </summary>
            private static List<string> usedInterps = new List<string>();

            public DefString[] DefiningStrings { get; set; }
            public string Interpretation { get; set; }
            public bool Shown { get; set; } = false;

            /// <summary>
            /// Defines the method
            /// </summary>
            public Method(DefString[] defStrings, string interp) {
                DefiningStrings = defStrings;
                Interpretation = interp;
            }

            /// <summary>
            /// Checks if the value given checks out with any existing methods
            /// and increments their defining strings seen counter if necessary.
            /// Also checks for already used interps
            /// </summary>
            public bool Check(string value)
            {
                if (usedInterps.Any(p => p == Interpretation))
                    return false;

                for (int i = 0; i < DefiningStrings.Length; i++)
                    if (DefiningStrings[i].Definition == value)
                        DefiningStrings[i].Seen = true;

                if (DefiningStrings.All(p => p.Seen))
                {
                    usedInterps.Add(Interpretation);
                    return true;
                }

                return false;
            }

            /// <summary>
            /// Parses a method from the ManagedMethods.txt format
            /// defStr1+defStr2+defStr3|interpretation
            /// </summary>
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

            /// <summary>
            /// The basic defining string structure
            /// </summary>
            public struct DefString
            {
                public string Definition;
                public bool Seen;
                public DefString(string def) { Definition = def; Seen = false;  }
            }
        }
    }
}
