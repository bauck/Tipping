using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tipping.Models
{
    public class TipsViewModel
    {
        public List<MergedKampOgTipsData> KampOgTips;
    }

    public class MergedKampOgTipsData
    {
        public int KampID { get; set; }
        public string BrukerID { get; set; }
        public string Hjemmelag { get; set; }
        public string Bortelag { get; set; }
        public DateTime Avspark { get; set; }
        public bool ErFerdigspilt { get; set; }
        public bool ErLevert { get; set; }
        public int MålHjemmelag { get; set; }
        public int MålBortelag { get; set; }
        public bool ErBeregnet { get; set; }
        public int Poeng { get; set; }
        public bool TipsfristUtløpt
        {
            get { return DateTime.Now > Avspark; }
        }
    }
}