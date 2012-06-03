using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tipping.Domain;

namespace Tipping.Models
{
    public class ResultatKampViewModel
    {
        public Kamp Kamp;
        public bool VisTips;
        public List<Tips> KampTips;
    }

}