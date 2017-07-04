using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stranalysis.Core
{
    public class Module
    {
        private readonly string name;

        public Module (string name) {
           this.name = name;
        }

        public virtual void Run(List<string> strings) {
            Program.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Program.WriteLine($"{name}\n");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
