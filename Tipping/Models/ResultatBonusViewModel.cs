using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tipping.Domain;

namespace Tipping.Models
{
    public class ResultatBonusViewModel
    {
        public Bonus Bonus;
        public bool VisTips;
        public List<BonusTips> BonusTips;
    }

}