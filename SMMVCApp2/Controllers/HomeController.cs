
using LibraryForMVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMMVCApp2.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        private IRootService _root;

        public HomeController(IRootService rs) { this._root = rs; }
        public ActionResult Index()
        {
            string msg = _root.WhoAmI() + "  :    " + _root.Child.TellMe();
            ViewBag.msg = msg;
            return View();
        }

    }
}
