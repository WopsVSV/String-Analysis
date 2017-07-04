using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace stranalysis.Core
{
    /// <summary>
    /// The main class which contains the application entry point
    /// </summary>
    public class Program
    {
        private static string log = string.Empty;

        /// <summary>
        /// The entry point of the application
        /// </summary>
        /// <param name="args">The arguments passed through the command line</param>
        static void Main(string[] args)
        {
            Console.Title = "String Analysis";
            Console.ForegroundColor = ConsoleColor.Red;
            Program.WriteLine("String Analysis - by Wops\n");
            Console.ForegroundColor = ConsoleColor.Gray;

            // Checks the existence of necessary assemblies
            if (!StringExtractorExists())
            {
                Console.WriteLine("Could not find the strings32.exe and strings64.exe assemblies in the executing directory");
                return;
            }

            // Checks for the help signal
            if (args.Length == 0 || args[0] == "-h" || args[0] == "-help")
            {
                Console.WriteLine(Properties.Resources.HelpDocument);
                Console.ReadKey(true);
                return;
            }

            // Parse arguments and set flags
            ParseArguments(args);

            // Choose the strings assembly version
            var extractorFile = Environment.Is64BitOperatingSystem ? "strings64.exe" : "strings32.exe";

            // Check for the file name argument
            string inputFile = args[args.Length - 1];
            if (!File.Exists(inputFile)) {
                Console.WriteLine("Could not find the file argument. Please use -h to get the help displayed.");
                return;
            }

            // Begin the analysis
            var analyser = new Analyser(extractorFile, inputFile);
            analyser.Run();

            // Log if necessary
            if (Flags.LogOutput)
                File.WriteAllText(inputFile + "_log.txt", log);
           
            Console.ReadKey(true);
        }

        /// <summary>
        /// Checks the existence of the strings32 and strings64 assemblies
        /// </summary>
        private static bool StringExtractorExists()
        {
            string executingDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return File.Exists(executingDirectory + "/strings32.exe") &&
                   File.Exists(executingDirectory + "/strings64.exe");
        }

        /// <summary>
        /// Parses the arguments given and sets flags accordingly
        /// </summary>
        private static void ParseArguments(string[] args)
        {
            foreach (var argument in args)
            {
                switch (argument)
                {
                    case "-dump":
                        Flags.DumpStrings = true; break;
                    case "-log":
                        Flags.LogOutput = true; break;
                }
            }
        }

        /// <summary>
        /// Writes to the console with consideration to the log flag
        /// </summary>
        public static void WriteLine(string text) {
            Console.WriteLine(text);

            if (Flags.LogOutput)
                log += text + '\n';
        }
        public static void WriteLine() {
            Console.WriteLine();
            if (Flags.LogOutput)
                log += '\n';
        }
    }
}