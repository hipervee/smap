using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMMVCApp1.Services
{
    public interface IRootService
    {
        IChildService Child {get;}
        string WhoAmI();
    }
    public interface IChildService
    {
        string TellMe();
    }

    public class RootService1 : IRootService
    {
        protected IChildService _child;
        public RootService1(IChildService cs)
        {
            this._child = cs;
        }
        public string WhoAmI()
        {
            return "Root Service 1";
        }

        public IChildService Child
        {
            get { return _child; }
        }
    }

    public class RootService2 : IRootService
    {
        protected IChildService _child;
        public RootService2(IChildService cs)
        {
            this._child = cs;
        }
        public string WhoAmI()
        {
            return "Root Service 2";
        }

        public IChildService Child
        {
            get { return _child; }
        }
    }

    public class ChildService1 : IChildService
    {

        public string TellMe()
        {
            return "I am Child 1 Service";
        }
    }

    public class ChildService2 : IChildService
    {

        public string TellMe()
        {
            return "I am Child 2 Service";
        }
    }
}