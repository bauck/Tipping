using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tipping.Data;
using Tipping.Domain;
using Tipping.Models;

namespace Tipping.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
                RedirectToAction("Login", "Account");
            if (User.Identity.Name.ToLower() != "admin")
                RedirectToAction("Index", "Home");

            var model = new TipsViewModel();

            var bonusSpm = DataAksessor.HentAlleBonus();
            var kamper = DataAksessor.HentAlleKamper();
            model.BonusOgTips = bonusSpm.Select(bonus => new MergedBonusOgTipsData
                                                             {
                                                                 BonusID = bonus.ID,
                                                                 Spørsmål = bonus.Spørsmål,
                                                                 Svar = bonus.Svar,
                                                                 Frist = bonus.Frist
                                                             }).ToList();

            model.KampOgTips = kamper.Select(kamp => new MergedKampOgTipsData
                                                         {
                                                             KampID = kamp.ID,
                                                             Avspark = kamp.Avspark,
                                                             Bortelag = kamp.Bortelag,
                                                             ErFerdigspilt = kamp.ErFerdigspilt,
                                                             Hjemmelag = kamp.Hjemmelag,
                                                             MålBortelag = kamp.MålBortelag,
                                                             MålHjemmelag = kamp.MålHjemmelag
                                                         }).ToList();

            return View(model);
        }


        [HttpPost]
        public JsonResult LagreResultat(int kampID, int målHjemmelag, int målBortelag)
        {
            if (!User.Identity.IsAuthenticated || User.Identity.Name.ToLower() != "admin")
                throw new Exception("Bruker er ikke logget inn.");
            var kamp = DataAksessor.LagreResultatForKamp(kampID, målHjemmelag, målBortelag);
            

            var kamptips = DataAksessor.HentAlleTipsForKamp(kampID);
            foreach (var tips in kamptips)
            {
                PoengBeregner.BerengPoeng(kamp, tips);
                DataAksessor.LagrePoengForTips(kamp.ID, tips.TipperID, tips.Poeng);
            }

            return Json(kamp, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult LagreBonusResultat(int bonusID, string svar)
        {
            if (!User.Identity.IsAuthenticated || User.Identity.Name.ToLower() != "admin")
                throw new Exception("Bruker er ikke logget inn.");

            var bonus = DataAksessor.LagreResultatForBonus(bonusID, svar);

            var bonustips = DataAksessor.HentAlleTipsForBonus(bonusID);
            foreach (var tips in bonustips)
            {
                PoengBeregner.BerengBonusPoeng(bonus, tips);
                DataAksessor.LagrePoengForBonusTips(bonus.ID, tips.TipperID, tips.Poeng);
            }

            return Json(bonus, JsonRequestBehavior.DenyGet);
        }
    }
}
