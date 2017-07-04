using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stranalysis.Core
{
    /// <summary>
    /// Base class for all modules
    /// </summary>
    public class Module
    {
        private readonly string name;

        /// <summary>
        /// Initialises the module with a name
        /// </summary>
        public Module (string name) {
           this.name = name;
        }
        
        /// <summary>
        /// Writes the module name before running the child modue
        /// </summary>
        public virtual void Run(List<string> strings) {
            Program.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Program.WriteLine($"{name}\n");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
