using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Tipping.Data;
using Tipping.Models;

namespace Tipping.Controllers
{
    public class ResultatController : Controller
    {
        //
        // GET: /Resultat/

        public ActionResult Index()
        {
            var tips = DataAksessor.HentAlleTips();
            var bonustips = DataAksessor.HentAlleBonusTips();
            var kamper = DataAksessor.HentAlleKamper();
            var bonusspørsmål = DataAksessor.HentAlleBonus();
            var viewModel = new ResultatViewModel();
            var perBruker = tips.GroupBy(ettTips => ettTips.TipperID);
            var bonusTipsPerBruker = bonustips.GroupBy(ettTips => ettTips.TipperID);
            var poengForKamper = perBruker.Select(bruker => new BrukerMedScore {Brukernavn = bruker.Key, Score = bruker.Sum(en => en.Poeng)}).OrderByDescending(bruker => bruker.Score).ToList();
            var poengForBonus = bonusTipsPerBruker.Select(bruker => new BrukerMedScore { Brukernavn = bruker.Key, Score = bruker.Sum(en => en.Poeng) }).OrderByDescending(bruker => bruker.Score).ToList();
            var totalpoeng = poengForKamper.Select(brukerMedScore => new BrukerMedScore { Brukernavn = brukerMedScore.Brukernavn, Score = brukerMedScore.Score + poengForBonus.First(b => b.Brukernavn.ToLower() == brukerMedScore.Brukernavn.ToLower()).Score }).OrderByDescending(bruker => bruker.Score).ToList();
            viewModel.TotalPoengsum = totalpoeng;
            viewModel.KamperMedFirePoeng = perBruker.Select(bruker => new BrukerMedScore { Brukernavn = bruker.Key, Score = bruker.Count(en => en.Poeng == 4) }).OrderByDescending(bruker => bruker.Score).ToList();
            viewModel.KamperMedPoeng = perBruker.Select(bruker => new BrukerMedScore { Brukernavn = bruker.Key, Score = bruker.Count(en => en.Poeng > 0) }).OrderByDescending(bruker => bruker.Score).ToList();
            var perKamp = tips.GroupBy(ettTips => ettTips.KampID);
            var kamperMedPoeng = perKamp.Select(kamp => new KeyValuePair<int, int> (kamp.Key, kamp.Sum(en => en.Poeng))).OrderByDescending(en => en.Value);
            var maxScore = kamperMedPoeng.Max(k => k.Value);
            var minScore = kamperMedPoeng.Min(k => k.Value);

            var maxKamper = kamperMedPoeng.Where(k => k.Value == maxScore);
            viewModel.KampMedFlestPoeng = (from maxKamp in maxKamper let kamp = kamper.First(k => k.ID == maxKamp.Key) select new KampMedScore { Lag = kamp.Hjemmelag + " - " + kamp.Bortelag, Score = maxKamp.Value, ID = kamp.ID }).ToList();
            var minKamper = kamperMedPoeng.Where(k => k.Value == minScore);
            viewModel.KampMedFærrestPoeng = (from minKamp in minKamper let kamp = kamper.First(k => k.ID == minKamp.Key) select new KampMedScore { Lag = kamp.Hjemmelag + " - " + kamp.Bortelag, Score = minKamp.Value, ID = kamp.ID }).ToList();
            viewModel.AlleKamper = kamper;
            viewModel.AlleBonusspørsmål = bonusspørsmål;

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Kamp(int id)
        {
            var kamp = DataAksessor.HentKamp(id);
            var kamptips = DataAksessor.HentAlleTipsForKamp(id).OrderByDescending(tips => tips.Poeng).ToList();

            var model = new ResultatKampViewModel { Kamp = kamp, KampTips = kamptips, VisTips = kamp.Avspark < DateTimeUtils.CETNå() };

            return View(model);

        }

        [HttpGet]
        public ActionResult Bonus(int id)
        {
            var bonus = DataAksessor.HentBonus(id);
            var bonustips = DataAksessor.HentAlleTipsForBonus(id).OrderByDescending(tips => tips.Poeng).ToList();

            var model = new ResultatBonusViewModel { Bonus = bonus, BonusTips = bonustips, VisTips = bonus.Frist < DateTimeUtils.CETNå() };

            return View(model);

        }

        [HttpGet]
        public ActionResult Bruker(string id)
        {
            var kamper = DataAksessor.HentAlleKamper();
            var tips = DataAksessor.HentAlleTipsForBruker(id);
            var tipsmodel = new TipsViewModel {Brukernavn = id, KampOgTips = new List<MergedKampOgTipsData>(), BonusOgTips = new List<MergedBonusOgTipsData>() };

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
                    MålBortelag = tippetips != null ? tippetips.MålBortelag : 0,
                    MålHjemmelag = tippetips != null ? tippetips.MålHjemmelag : 0,
                    Poeng = tippetips != null ? tippetips.Poeng : 0
                });
            }
            var bonus = DataAksessor.HentAlleBonus();
            var bonusTips = DataAksessor.HentAlleBonusTipsForBruker(id);

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
    }
}
