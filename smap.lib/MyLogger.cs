using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smap.lib
{
   public class MyLogger : ILogger
    {

        public MyLogger()
        {
            Console.WriteLine("My Logger Used");
        }

        public void toConsole(string message)
        {
            Console.WriteLine("{0}" + message, "MyLogger");
        }
    }
}
