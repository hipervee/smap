using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smap.lib
{
    public interface IMyCounter
    {
        void inc();
        void show();
    }


    public class MyCounter : IMyCounter
    {
        private int _cnt;
        public void inc() { _cnt++; }
        public void show() { Console.WriteLine("counter: {0}", _cnt); }

    }

    public class SuperCounter : IMyCounter
    {
        private int _cnt;
        public void inc() { _cnt+=10; }
        public void show() { Console.WriteLine("counter: {0}", _cnt); }
    }
}
