using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using stranalysis.Managed_Modules;
using stranalysis.Shared_Modules;

namespace stranalysis.Core
{
    /// <summary>
    /// The class that analyses the strings
    /// </summary>
    public class Analyser
    {
        private string extractorFile; // The assembly needed to extract strings
        private readonly string inputFile;     // The assembly to be analysed
        private List<string> strings; // The actual string list
        private readonly Extractor extractor;

        // Modules
        private readonly List<Module> managedModules;
        private readonly List<Module> unmanagedModules;
        private readonly List<Module> sharedModules;

        /// <summary>
        /// Assigns the private fields extractorFile and inputFile values
        /// </summary>
        public Analyser(string extractorFile, string inputFile) {
            this.extractorFile = extractorFile;
            this.inputFile = inputFile;
            
            extractor = new Extractor(extractorFile);

            // Define module lists
            managedModules = new List<Module>();
            unmanagedModules = new List<Module>();
            sharedModules = new List<Module>();

            // Define shared modules
            sharedModules.Add(new LinksPathsModule());
            sharedModules.Add(new BlacklistModule());
            sharedModules.Add(new MiscellaneousModule());

            // Define managed modules
            managedModules.Add(new ManagedResourceModule());
            managedModules.Add(new ManagedMethodsModule());
            managedModules.Add(new PInvokeModule());

            // Define unmanaged modules
        }

        /// <summary>
        /// Executes the analysis
        /// </summary>
        public void Run() {

            // Accept the eula so the process can run
            extractor.AcceptEula();

            // Get the strings
            strings = extractor.GetStrings(inputFile);

            // Write and get modules
            var moduleList = WriteAndGetModules();

            // Runs the specific modules
            foreach (var module in moduleList)
                module.Run(strings);
            

            // Runs the shared modules
            foreach (var module in sharedModules)
                module.Run(strings);
            


        }

        /// <summary>
        /// Writes the phase 1 text
        /// </summary>
        private List<Module> WriteAndGetModules() {

            AssemblyInfo asmInfo;
            try {
                asmInfo = AssemblyHelper.GetAssemblyInfo(inputFile);
            } catch {
                asmInfo = new AssemblyInfo {SizeKB = "Error", Type = "Error"};
            }

            // TODO: REMOVE
            char input = Console.ReadKey(true).KeyChar;
            asmInfo.Type = input == 'm' ? "Managed" : "Unmanaged";
            asmInfo.SizeKB = "127 kb";

            Program.WriteLine($"Assembly type: {asmInfo.Type}");
            Program.WriteLine($"Assembly size: {asmInfo.SizeKB}");
            Program.WriteLine($"Strings extracted: {strings.Count}\n");
            Program.WriteLine($"Executing {asmInfo.Type.ToLower()} modules...");

            return asmInfo.Type == "Managed" ? managedModules : unmanagedModules;
        }
    }
}
