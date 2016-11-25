using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap;
using smap.lib;
using StructureMap.Pipeline;
using StructureMap.Graph;
using StructureMap.Graph.Scanning;
using smap.LifeCycles;
using System.Threading;

namespace smap
{
    class Program
    {
        static void Main(string[] args)
        {
            //defaultRegistrations();
            //namedRegistration();
            //simpleNamedRegistration();
            //usingRegistries();
            //AssemblyScanAndAutoRegister();
            //SetterReg();
            //UseCustomConvention();
            //usingLifeCycle();
            //usingConsumerCounter();
            //usingCustomTimeExpiringLifeCycle();
            //usingChildContainer();
            usingNestedContainer();
            Console.Read();
        }

        static void sep(string title)
        {
            Console.WriteLine("========================{0}================================", title);
        }

        static void usingProfile()
        {
            var c = new Container();
            c.Configure(_ =>
           {
               _.For<IMyCounter>().Use<MyCounter>();

               _.Profile("p1", x =>
               {
                   x.For<IMyCounter>().Use<SuperCounter>();
               });
           });


            var c1 = c.GetInstance<IMyCounter>();
        }

        static void usingNestedContainer()
        {
            sep("usingNestedContainer");

            var c = new Container(_ => {
                _.For<IWorker>().Use<Worker>();
            });

            using (var nested = c.GetNestedContainer())
            {
                var worker = nested.GetInstance<IWorker>();
                worker.work();
            }
        }



        static void usingChildContainer()
        {
            sep("usingChildContainer");

            var p = new Container();
            p.Configure(c => {
                c.For<IMyCounter>().Singleton().Use<MyCounter>();
            });

            var counter = p.GetInstance<IMyCounter>();
            counter.inc(); counter.inc(); counter.show();

            var ch = p.CreateChildContainer();
            ch.Configure(x =>
            {
                x.For<IMyCounter>().Use<SuperCounter>();
            });
            var childCounter = ch.GetInstance<IMyCounter>();
            childCounter.inc(); childCounter.inc(); childCounter.show();

        }

        static void usingCustomTimeExpiringLifeCycle()
        {
            sep("usingCustomTimeExpiringLifeCycle");
            var c = new Container( r => r.For<IMyCounter>().LifecycleIs(new TimeExpiringLifeCycle(5)).Use<MyCounter>());
            IMyCounter v = c.GetInstance<IMyCounter>();
            v.inc(); v.inc(); v.show();
            Thread.Sleep(3000);
            v = c.GetInstance<IMyCounter>();
            v.inc(); v.inc(); v.show();
            Thread.Sleep(3000);
            v = c.GetInstance<IMyCounter>();
            v.inc(); v.inc(); v.show();

        }

        static void usingConsumerCounter()
        {
            sep("usingConsumerCounter");
            var c = new Container();
            c.Configure(r =>
            {
                r.For<IMyCounter>().AlwaysUnique().Use<MyCounter>();
            });

            var cc = c.GetInstance<CounterConsumer>();

            cc.c1.inc();
            cc.c1.show();
        }

        static void usingLifeCycle()
        {
            sep("Using LifeCycle");
            var c = new Container();
            c.Configure(r =>
            {
                r.For<IMyCounter>().Singleton().Use<MyCounter>();

            });

            var v1 = c.GetInstance<IMyCounter>();
            var v2 = c.GetInstance<IMyCounter>();
            v1.inc(); v1.inc(); v1.show();
            v2.inc(); v2.inc(); v2.show();

            Console.WriteLine("V1==V2 {0}", v1 == v2);

        }

        static void usingSpecificObject()
        {
            sep("Using Specific Object -- [Use Instance]");

            var container = new Container(x =>
            {
                var logger = new Logger();
                x.For<ILogger>().Use(logger);
            });
        }



        static void useIfNone()
        {
            sep("Use if the Required refrenence Variable is NULL-- [UseIfNone]");

        }

        static void SetterReg()
        {
            sep("Setter Reg");
            var c = new Container();

            c.Configure(r =>
            {

                r.For<IPerson>().Use<Person>()
                .Setter<string>("Name").Is("Pervaze")
                .Setter<double>("Salary").Is(20000)
                .Setter<Address>().Is(new Address()
                {
                    City = "Mumbai",
                    State = "Mah"
                });
            });

            var p = c.GetInstance<IPerson>();
            Console.WriteLine(p.Name);
            Console.WriteLine(p.Salary);
            Console.WriteLine(p.Address.City);

        }

        static void UseCustomConvention()
        {
            sep("Using Custom Convention");
            var c = new Container();
            c.Configure(r =>
            {
                r.Scan(scanner =>
                {
                    scanner.AssembliesAndExecutablesFromApplicationBaseDirectory();
                    scanner.Convention<MyConvention>();
                });

            });

            var logger = c.GetInstance<ILogger>();
            var superLogger = c.GetInstance<ILogger>("SuperLogger.SuperLogger");
            logger.toConsole("Hey");
            superLogger.toConsole("SuperMan");


        }


        public class MyConvention : IRegistrationConvention
        {
            public void ScanTypes(TypeSet types, Registry registry)
            {
                foreach (Type type in types.FindTypes
                      (TypeClassification.Concretes | TypeClassification.Closed))
                {

                    if (type.Namespace.StartsWith("smap.lib"))
                    {
                        foreach (Type itype in type.GetInterfaces())
                        {
                            Console.WriteLine(itype.ToString() + "-" + type.ToString());
                            registry.For(itype).Use(type);
                        }
                    }
                    else if (type.Namespace.StartsWith("SuperLogger"))
                    {
                        Console.WriteLine("==>" + type.ToString());
                        foreach (Type itype in type.GetInterfaces())
                        {
                            Console.WriteLine(itype.ToString() + "-" + type.ToString());
                            registry.For(itype).Add(type).Named(type.ToString());
                        }
                    }
                }
            }
        }


        static void AssemblyScanAndAutoRegister()
        {
            sep("AssemblyScanAndAutoRegister");
            var c = new Container();
            c.Configure(x =>
            {
                x.Scan(scanner =>
                {
                    // scanner.AssembliesFromApplicationBaseDirectory();
                    //scanner.WithDefaultConventions();
                    scanner.Assembly("SuperLogger");
                    scanner.SingleImplementationsOfInterface();
                });
            });

            var logger = c.GetInstance<ILogger>();
            logger.toConsole("Hey");
        }

        static void usingRegistries()
        {
            sep("Use Registries");


            var c = new Container(new MainRegistry());
            var logger = c.GetInstance<Logger>();
            var car = c.GetInstance<Car>();

            car.Start();
            car.ChangeGear(3);
            car.ON();
            car.OFF();
            car.Stop();

            var car2 = c.GetInstance<Car>();

            sep("Check If same Car Instance");
            var areCarsEqual = car.GetEngine() == car2.GetEngine();
            logger.toConsole("Car Equality is " + areCarsEqual.ToString());

        }

        static void usingAutoRegistrations()
        {
            var c = new Container(x =>
            {

            });
        }


        static void simpleNamedRegistration()
        {
            sep("Simplified Named Regs");
            var cont = new Container();
            cont.Configure(x =>
            {
                x.For<IEngine>().Use<AudiEngine>();
                x.For<IBattery>().Use<ExideBattery>();
                x.For<Car>().Use<Car>();
                x.For<IGearBox>().Use<MahindraGearBox>();

                x.For<Car>().Add<Car>().Named("car2")
                 .Ctor<IEngine>().Is<MercedezEngine>()
                 .Ctor<IBattery>().Is<AmazonBattery>();


            });

            var c1 = cont.GetInstance<Car>();

            c1.Start();
            c1.Stop();
            c1.ChangeGear(1);

            var c2 = cont.GetInstance<Car>("car2");

            c2.Start();
            c2.Stop();
            c2.ChangeGear(2);
        }

        static void namedRegistration()
        {
            sep("Named Regs");
            var cont = new Container();
            cont.Configure(x =>
            {
                x.For<IEngine>().Use<AudiEngine>();
                x.For<IBattery>().Use<ExideBattery>();
                x.For<Car>().Use<Car>();
                x.For<IGearBox>().Use<MahindraGearBox>();

                x.For<IEngine>().Add<MercedezEngine>().Named("merc");
                x.For<IBattery>().Add<AmazonBattery>().Named("amz");

            });

            var c1 = cont.GetInstance<Car>();

            c1.Start();
            c1.Stop();
            c1.ChangeGear(1);

            var mercEngine = cont.GetInstance<IEngine>("merc");
            var amzBattery = cont.GetInstance<IBattery>("amz");

            var c2 = new Car(mercEngine, amzBattery);

            c2.Start();
            c2.Stop();
            c2.ChangeGear(2);
        }

        static void defaultRegistrations()
        {
            sep("Default Regs");

            var container = new Container(x => x.AddRegistry<CarRegistry>());
            Car c = container.GetInstance<Car>();
            c.Start();
            c.Stop();
            c.ChangeGear(2);

        }

        public class CarRegistry : Registry
        {
            public CarRegistry()
            {
                For<Car>().Use<Car>();
                For<IEngine>().Use<AudiEngine>();
                For<IBattery>().Use<ExideBattery>();
                For<IGearBox>().Use<MahindraGearBox>();
            }
        }
    }
}
