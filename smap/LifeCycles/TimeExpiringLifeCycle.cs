using StructureMap.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap;

namespace smap.LifeCycles
{
    public class TimeExpiringLifeCycle : ILifecycle
    {
        private readonly long _secondsToExpire;
        private readonly IObjectCache _cache = new LifecycleObjectCache();

        private DateTime _lastExpired;

        public string Description
        {
            get
            {
                return "TimeExpiringLifeCycle";
            }
        }

        public TimeExpiringLifeCycle(long timeInSeconds)
        {
            _secondsToExpire = timeInSeconds;
            _cache.DisposeAndClear();
        }

        private void Expire()
        {
            _lastExpired = DateTime.Now;
            _cache.DisposeAndClear();
        }

        public void EjectAll(ILifecycleContext context)
        {
            _cache.DisposeAndClear();
        }

        public IObjectCache FindCache(ILifecycleContext context)
        {
            if (DateTime.Now.AddSeconds(-_secondsToExpire) >= _lastExpired)
                Expire();

            return _cache;
        }
    }
}
