using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smap.lib
{
    public interface IWorker
    {
        void work();
    }

    public class Worker: IWorker, IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine("Disposed");
        }

        public void work()
        {
            Console.WriteLine("I Did some work");
        }
    }
}
