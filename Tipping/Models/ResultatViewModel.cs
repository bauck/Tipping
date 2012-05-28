using System.Collections.Generic;

namespace Tipping.Models
{
    public class ResultatViewModel
    {
        public IEnumerable<BrukerMedScore> TotalPoengsum { get; set; }
        public IEnumerable<BrukerMedScore> KamperMedFirePoeng { get; set; }
        public IEnumerable<BrukerMedScore> KamperMedPoeng { get; set; }
        public IEnumerable<KampMedScore> KampMedFlestPoeng { get; set; }
        public IEnumerable<KampMedScore> KampMedFærrestPoeng { get; set; }
    }

    public class KampMedScore
    {
        public string Lag { get; set; }
        public int Score { get; set; }
    }

    public class BrukerMedScore
    {
        public string Brukernavn { get; set; }
        public int Score { get; set; }
    }
}