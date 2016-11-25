using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smap.lib
{
    public interface IEngine
    {
        IGearBox GearBox { get; set; }
        void Start();
        void Stop();
    }

    public class MercedezEngine : IEngine
    {
        private IGearBox gb;
        public IGearBox GearBox
        {
            get
            {
                return this.gb;
            }

            set
            {
                this.gb = value;
            }
        }

        public MercedezEngine()
        {

        }

        public MercedezEngine(IGearBox g)
        {

            this.gb = g;
        }
        public void Start()
        {
            Console.WriteLine("Mercedez Engine Started");
        }

        public void Stop()
        {
            Console.WriteLine("Mercedez Engine Stopped");
        }

        
    }

    public class AudiEngine : IEngine
    {
        private IGearBox gb;
        public IGearBox GearBox
        {
            get
            {
                return this.gb;
            }

            set
            {
                this.gb = value;
            }
        }
        public AudiEngine()
        {
        }

        public AudiEngine(IGearBox g)
        {

            this.gb = g;
        }
        public void Start()
        {
            Console.WriteLine("Audi Engine Started");
        }

        public void Stop()
        {
            Console.WriteLine("Audi Engine Stopped");
        }
    }

    public class AmazonBattery : IBattery
    {
        public void TurnOff()
        {
            Console.WriteLine("Amazon Battery turned Off");
        }

        public void TurnOn()
        {
            Console.WriteLine("Amazon Battery turned On");

        }
    }
    public class ExideBattery : IBattery
    {
        public void TurnOff()
        {
            Console.WriteLine("Exide Battery turned Off");
        }

        public void TurnOn()
        {
            Console.WriteLine("Exide Battery turned On");

        }
    }

    public interface IBattery
    {
        void TurnOn();
        void TurnOff();
    }

    public class MahindraGearBox : IGearBox
    {
        public void ChangeGear(int togearno)
        {
            Console.WriteLine("Changed Gear");
        }
    }


    public interface IGearBox
    {

        void ChangeGear(int togearno);
    }

    public class Car
    {
        private IEngine eng;
        private IBattery batt;

        public Car(IEngine e, IBattery b)
        {
            this.eng = e;this.batt = b;

        }

        public IEngine GetEngine()
        {
            return eng;
        }

       public void Start()
        {
            this.eng.Start();

        }
        public void Stop()
        {
            this.eng.Stop();

        }

        public void ON()
        {
            this.batt.TurnOn();

        }

        public void OFF()
        {
            this.batt.TurnOff();

        }

        public void ChangeGear(int grno)
        {
            this.eng.GearBox.ChangeGear(grno);

        }

        public override string ToString()
        {
            return "Hey";
        }
    }
}
