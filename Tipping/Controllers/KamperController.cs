using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tipping.Data;
using Tipping.Models;

namespace Tipping.Controllers
{
    public class KamperController : Controller
    {
        //
        // GET: /Kamper/

        public ActionResult Index()
        {
            var kamper = DataAksessor.HentAlleKamper();
            var model = new KamperViewModel {Kamper = kamper};
            return View(model);
        }
    }
}
