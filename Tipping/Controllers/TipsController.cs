using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using Tipping.Data;
using Tipping.Models;

namespace Tipping.Controllers
{
    public class TipsController : Controller
    {
        //
        // GET: /Tips/

        public ActionResult Index()
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("Login", "Account");
            var kamper = DataAksessor.HentAlleKamper();
            var tips = DataAksessor.HentAlleTipsForBruker(User.Identity.Name);
            var tipsmodel = new TipsViewModel{KampOgTips = new List<MergedKampOgTipsData>(), BonusOgTips = new List<MergedBonusOgTipsData>()};

            foreach (var kamp in kamper)
            {
                var tippetips = tips.FirstOrDefault(en => en.KampID == kamp.ID);
                tipsmodel.KampOgTips.Add(new MergedKampOgTipsData
                                             {
                                                 Avspark = kamp.Avspark,
                                                 Bortelag = kamp.Bortelag,
                                                 BrukerID = User.Identity.Name,
                                                 ErBeregnet = tippetips != null && tippetips.ErBeregnet,
                                                 ErFerdigspilt = kamp.ErFerdigspilt,
                                                 ErLevert = tippetips != null && tippetips.ErLevert,
                                                 Hjemmelag = kamp.Hjemmelag,
                                                 KampID = kamp.ID,
                                                 MålBortelag = tippetips!= null ? tippetips.MålBortelag : 0,
                                                 MålHjemmelag = tippetips != null ? tippetips.MålHjemmelag : 0,
                                                 Poeng = tippetips != null ? tippetips.Poeng : 0
                                             });
            }
            var bonus = DataAksessor.HentAlleBonus();
            var bonusTips = DataAksessor.HentAlleBonusTipsForBruker(User.Identity.Name);

            foreach (var spm in bonus)
            {
                var bonustips = bonusTips.FirstOrDefault(en => en.BonusID == spm.ID);
                tipsmodel.BonusOgTips.Add(new MergedBonusOgTipsData
                                              {
                                                  BonusID = spm.ID,
                                                  BrukerID = User.Identity.Name,
                                                  ErBeregnet = bonustips != null && bonustips.ErBeregnet,
                                                  ErFerdigspilt = spm.ErFerdigspilt,
                                                  ErLevert = bonustips != null && bonustips.ErLevert,
                                                  Frist = spm.Frist,
                                                  Poeng = bonustips != null ? bonustips.Poeng : 0,
                                                  Spørsmål = spm.Spørsmål,
                                                  Svar = bonustips != null ? bonustips.Svar : string.Empty
                                              });
            }

            return View(tipsmodel);
        }

        [HttpPost]
        public JsonResult LagreTips(int kampID, int målHjemmelag, int målBortelag)
        {
            if (!Request.IsAuthenticated)
                throw new Exception("Bruker er ikke logget inn.");
            var kamp = DataAksessor.HentKamp(kampID);
            if (kamp.Avspark < DateTime.Now)
                throw new Exception("Kamp har allerede startet.");

            var lagretTips = DataAksessor.LagreTips(kampID, User.Identity.Name, målHjemmelag, målBortelag);
            return Json(lagretTips, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult LagreBonusTips(int bonusID, string svar)
        {
            if (!Request.IsAuthenticated)
                throw new Exception("Bruker er ikke logget inn.");
            var bonus = DataAksessor.HentBonus(bonusID);
            if (bonus.Frist < DateTime.Now)
                throw new Exception("Fristen for spørsmål er passert.");

            var lagretTips = DataAksessor.LagreBonusTips(bonusID, User.Identity.Name, svar);
            return Json(lagretTips, JsonRequestBehavior.DenyGet);
        }

    }
}
