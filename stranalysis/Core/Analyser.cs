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
        private string extractorFile;           // The assembly needed to extract strings
        private readonly string inputFile;      // The assembly to be analysed
        private List<string> strings;           // The actual string list
        private readonly Extractor extractor;   // The extraction component

        // Modules
        private readonly List<Module> managedModules;
        private readonly List<Module> unmanagedModules;
        private readonly List<Module> sharedModules;
        private readonly List<List<Module>> modules;

        /// <summary>
        /// Assigns the private fields extractorFile and inputFile values
        /// </summary>
        public Analyser(string extractorFile, string inputFile) {
            this.extractorFile = extractorFile;
            this.inputFile = inputFile;
            
            extractor = new Extractor(extractorFile);
            
            // Define shared modules
            sharedModules = new List<Module>
            {
                new LinksPathsModule(),
                new BlacklistModule(),
                new MiscellaneousModule()
            };

            // Define managed modules
            managedModules = new List<Module>
            {
                new ManagedResourceModule(),
                new ManagedMethodsModule(),
                new PInvokeModule()
            };

            // Define unmanaged modules
            unmanagedModules = new List<Module>
            {

            };

            // Define the module list
            modules = new List<List<Module>>
            {
                sharedModules,
                managedModules,
                unmanagedModules
            };
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
            // TODO : REWRITE
            var moduleList = WriteAndGetModules();

            // Runs the modules
            foreach (var listModuleTypes in modules)
                foreach(var module in listModuleTypes)
                    module.Run(strings);

        }

        /// <summary>
        /// Writes text to console
        /// </summary>
        private List<Module> WriteAndGetModules() {

            AssemblyInfo asmInfo;
            try {
                asmInfo = AssemblyHelper.GetAssemblyInfo(inputFile);
            } catch {
                asmInfo = new AssemblyInfo { SizeKB = "Error", Type = "Error" };
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
