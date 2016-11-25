using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smap.lib
{
    public class Logger : ILogger
    {

        public  Logger()
        {
            Console.WriteLine("Using Logger");
        }
        public void toConsole(string message)
        {
            Console.WriteLine("{0} " + message, "Logger");
        }
    }
}
