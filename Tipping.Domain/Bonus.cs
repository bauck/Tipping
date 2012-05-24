using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tipping.Domain
{
    public class Bonus
    {
        public Bonus(int id, string spørsmål, string svar, DateTime frist, bool erFerdigspilt)
        {
            ID = id;
            Spørsmål = spørsmål;
            Svar = svar;
            Frist = frist;
            ErFerdigspilt = erFerdigspilt;
        }

        public int ID { get; set; }
        public string Spørsmål { get; set; }
        public string Svar { get; set; }
        public DateTime Frist { get; set; }
        public Boolean ErFerdigspilt { get; set; }
    }
}
