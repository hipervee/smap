using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap;
using smap.lib;

namespace smap
{
    public class MainRegistry : Registry
    {
        public MainRegistry()
        {
            var logger = new Logger();
            this.For<ILogger>().Use(logger);
            this.IncludeRegistry<EngineRegistry>();
            this.IncludeRegistry<GearBoxRegistry>();
        }
    }

    public class EngineRegistry : Registry
    {
        public EngineRegistry()
        {
            var engine = new MercedezEngine();
            this.For<IEngine>().Use(engine);
            this.For<IBattery>().Use<AmazonBattery>();
            this.For<IBattery>().UseIfNone<ExideBattery>();
        }
    }

    public class GearBoxRegistry: Registry
    {
        public GearBoxRegistry()
        {
            this.For<IGearBox>().Use<MahindraGearBox>();
        }
    }
}
