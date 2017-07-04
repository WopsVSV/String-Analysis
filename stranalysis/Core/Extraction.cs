using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stranalysis.Core
{
    /// <summary>
    /// Class that helps with extracting the strings
    /// </summary>
    public class Extractor
    {
        private readonly string extractorFile;

        /// <summary>
        /// Assigns the extractorFile a value
        /// </summary>
        public Extractor(string extractorFile) {
            this.extractorFile = extractorFile;
        }

        /// <summary>
        /// Attemps to extract the strings and return them 
        /// </summary>
        public List<string> GetStrings(string inputFile) {

            // Declares the objects
            var extractionArguments = $"-nobanner {inputFile}";
            var extractionProcess = GetExtractionProcess(extractionArguments);
            var strings = new List<string>();
            var line = string.Empty;

            // Start the process and parse input through filters

            if (!inputFile.EndsWith(".txt")) {
                extractionProcess.Start();
                while (!extractionProcess.StandardOutput.EndOfStream) {
                    line = extractionProcess.StandardOutput.ReadLine();
                    if (StringFilter.Parse(line) != null)
                        strings.Add(line);
                }
            }
            else {
                using (var rdr = new StreamReader(inputFile))
                    while (!rdr.EndOfStream)
                        strings.Add(rdr.ReadLine());
            }

            return strings;
        }

        /// <summary>
        /// Accepts the eula of the strings assembly
        /// </summary>
        public void AcceptEula() {
            var eulaProcess = GetExtractionProcess("-accepteula");
            eulaProcess.Start();
            eulaProcess.WaitForExit();
        }

        /// <summary>
        /// Returns an extraction process with specific parameters
        /// </summary>
        private Process GetExtractionProcess(string arguments) {
            return new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = extractorFile,
                    Arguments = arguments,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };
        }
    }
}
