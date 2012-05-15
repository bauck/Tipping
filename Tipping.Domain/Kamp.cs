using System;

namespace Tipping.Domain
{
    public class Kamp
    {
        public Kamp(string hjemmelag, string bortelag, DateTime avspark)
        {
            Hjemmelag = hjemmelag;
            Bortelag = bortelag;
            Avspark = avspark;
        }

        public Kamp(int id, string hjemmelag, string bortelag, DateTime avspark, int målHjemmelag, int målBortelag, bool erFerdigspilt)
        {
            ID = id;
            Hjemmelag = hjemmelag;
            Bortelag = bortelag;
            Avspark = avspark;
            MålHjemmelag = målHjemmelag;
            MålBortelag = målBortelag;
            ErFerdigspilt = erFerdigspilt;
        }

        public int ID { get; set; }
        public DateTime Avspark { get; set; }
        public string Bortelag { get; set; }
        public string Hjemmelag { get; set; }
        public int MålHjemmelag { get; set; }
        public int MålBortelag { get; set; }
        public Boolean ErFerdigspilt { get; set; }
    }
}
