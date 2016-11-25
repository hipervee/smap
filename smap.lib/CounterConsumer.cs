using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smap.lib
{
    public  class CounterConsumer
    {
        public IMyCounter c1 { get; set; }
        public IMyCounter c2 { get; set; }

        public CounterConsumer(IMyCounter _c1, IMyCounter _c2)
        {
            this.c1 = _c1;
            this.c2 = _c2;
            c1.inc();
            c1.inc();
            c2.inc();
            c2.inc();
        }
    }
}
