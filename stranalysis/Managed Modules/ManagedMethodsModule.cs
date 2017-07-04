using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using stranalysis.Core;

namespace stranalysis.Managed_Modules
{
    /// <summary>
    /// The DotNetMethodsModule evaluates the methods found inside the strings and displays their general usage
    /// </summary>
    public partial class ManagedMethodsModule : Module
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

                // Checks if the string is empty or starts with // (comment)
                if (string.IsNullOrEmpty(line) || line.StartsWith(@"//") || line == "\r") continue;

                methods.Add(Method.ParseMethod(line));
            }

            // Evaluate strings
            for (int i = 0; i < strings.Count; i++) {
                for(int j = 0; j < methods.Count; j++)
                    if (methods[j].Check(strings[i])) {
                        Program.WriteLine(methods[j].Interpretation);
                    }
            }
        }
    }
}
