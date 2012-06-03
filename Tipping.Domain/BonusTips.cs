using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tipping.Domain
{
    public class BonusTips
    {
        public BonusTips(int bonusId, string brukernavn, string svar, bool erLevert, bool erBeregnet, int poeng)
        {
            BonusID = bonusId;
            TipperID = brukernavn;
            Svar = svar;
            ErLevert = erLevert;
            ErBeregnet = erBeregnet;
            Poeng = poeng;
        }

        public int BonusID { get; set; }
        public string TipperID { get; set; }
        public string Svar { get; set; }
        public int Poeng { get; set; }
        public Boolean ErBeregnet { get; set; }
        public Boolean ErLevert { get; set; }
    }
}
