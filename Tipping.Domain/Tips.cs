using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tipping.Domain
{
    public class Tips
    {
        public Tips(int kampID, string tipperID, int målHjemmelag, int målBortelag, bool erLevert)
        {
            KampID = kampID;
            TipperID = tipperID;
            MålHjemmelag = målHjemmelag;
            MålBortelag = målBortelag;
            ErLevert = erLevert;

            ErBeregnet = false;
            Poeng = 0;
        }

        public Tips(int kampID, string tipperID, int målHjemmelag, int målBortelag, bool erLevert, bool erBeregnet, int poeng)
        {
            KampID = kampID;
            TipperID = tipperID;
            MålHjemmelag = målHjemmelag;
            MålBortelag = målBortelag;
            ErLevert = erLevert;
            ErBeregnet = erBeregnet;
            Poeng = ErBeregnet ? poeng : 0;
        }
        public int KampID { get; set; }
        public string TipperID { get; set; }
        public int MålHjemmelag { get; set; }
        public int MålBortelag { get; set; }
        public bool ErLevert { get; set; }
        public bool ErBeregnet { get; set; }
        public int Poeng { get; set; }
    }
}
