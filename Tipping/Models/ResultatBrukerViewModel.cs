using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tipping.Domain;

namespace Tipping.Models
{
    public class ResultatBrukerViewModel
    {
        public string Brukernavn;
        public List<MergedKampOgTipsData> KampOgTips;
        public List<MergedBonusOgTipsData> BonusOgTips;
    }

}