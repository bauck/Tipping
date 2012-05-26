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

            return View(viewModel);
        }

    }
}
