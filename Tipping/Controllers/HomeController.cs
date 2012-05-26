using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tipping.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Sommerens vakreste eventyr.";

            return View();
        }

        public ActionResult Regler()
        {
            ViewBag.Message = "Slik kaprer du poengene.";

            return View();
        }

    }
}
