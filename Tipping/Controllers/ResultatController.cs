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
            var viewModel = new ResultatViewModel();
            var perBruker = tips.GroupBy(ettTips => ettTips.TipperID);
            viewModel.TotalPoengsum = perBruker.Select(bruker => new BrukerMedScore {Brukernavn = bruker.Key, Score = bruker.Sum(en => en.Poeng)}).OrderByDescending(bruker => bruker.Score).ToList();
            viewModel.KamperMedFirePoeng = perBruker.Select(bruker => new BrukerMedScore { Brukernavn = bruker.Key, Score = bruker.Count(en => en.Poeng == 4) }).OrderByDescending(bruker => bruker.Score).ToList();
            viewModel.KamperMedPoeng = perBruker.Select(bruker => new BrukerMedScore { Brukernavn = bruker.Key, Score = bruker.Count(en => en.Poeng > 0) }).OrderByDescending(bruker => bruker.Score).ToList();
            var perKamp = tips.GroupBy(ettTips => ettTips.KampID);
            var kamperMedPoeng = perKamp.Select(kamp => new KeyValuePair<int, int> (kamp.Key, kamp.Sum(en => en.Poeng))).OrderByDescending(en => en.Value);
            var maxScore = kamperMedPoeng.Max(k => k.Value);
            var minScore = kamperMedPoeng.Min(k => k.Value);

            var maxKamper = kamperMedPoeng.Where(k => k.Value == maxScore);
            viewModel.KampMedFlestPoeng = (from maxKamp in maxKamper let kamp = DataAksessor.HentKamp(maxKamp.Key) select new KampMedScore { Lag = kamp.Hjemmelag + " - " + kamp.Bortelag, Score = maxKamp.Value }).ToList();
            var minKamper = kamperMedPoeng.Where(k => k.Value == minScore);
            viewModel.KampMedFærrestPoeng = (from minKamp in minKamper let kamp = DataAksessor.HentKamp(minKamp.Key) select new KampMedScore { Lag = kamp.Hjemmelag + " - " + kamp.Bortelag, Score = minKamp.Value }).ToList();

            return View(viewModel);
        }

    }
}
