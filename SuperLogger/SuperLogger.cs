using smap;
using smap.lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperLogger
{
    public class SuperLogger : ILogger
    {
        public SuperLogger()
        {
            Console.WriteLine("Using Super Logger");
        }

        public void toConsole(string message)
        {
            Console.WriteLine("{0}" + message, "SuperLogger");
        }
    }
}
