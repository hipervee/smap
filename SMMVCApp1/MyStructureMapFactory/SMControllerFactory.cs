using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using StructureMap;
using SMMVCApp1.Services;

namespace SMMVCApp1.MyStructureMapFactory
{

    public class StructureMapControllerFactory : DefaultControllerFactory
    {
        private Container TheContainer;

        public StructureMapControllerFactory(Container cp)
        {
            this.TheContainer = cp;
        }

        protected override IController
            GetControllerInstance(RequestContext requestContext,
            Type controllerType)
        {
            try
            {
                if ((requestContext == null) || (controllerType == null))
                    return null;

                return (Controller)TheContainer.GetInstance(controllerType);
            }
            catch (StructureMapException)
            {
                System.Diagnostics.Debug.WriteLine(TheContainer.WhatDoIHave());
                throw new Exception(TheContainer.WhatDoIHave());
            }
        }
    }

    public static class Bootstrapper
    {
        public static Container TheContainer;
        public static void Run()
        {
            TheContainer = new Container();
            TheContainer.Configure(smr =>
            {
                
                smr.For<IRootService>().Use<RootService1>();
                smr.For<IChildService>().Use<ChildService1>();

            });
            ControllerBuilder.Current
                .SetControllerFactory(new StructureMapControllerFactory(TheContainer));

        }
    }
}