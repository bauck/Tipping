using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tipping.Domain
{
    public static class PoengBeregner
    {
        public static void BerengPoeng(Kamp kamp, Tips tips)
        {
            if (kamp.ID != tips.KampID || !kamp.ErFerdigspilt || !tips.ErLevert)
            {
                tips.Poeng = 0;
            } else if (TipsErEksaktLiktSomResultat(kamp, tips))
            {
                tips.Poeng = 4;
                tips.ErBeregnet = true;
            } else if (BådeTipsOgKampErUavgjort(kamp, tips))
            {
                tips.Poeng = 1;
                tips.ErBeregnet = true;
            } else if (TipsOgKampHarSammeDifferanse(kamp, tips))
            {
                tips.Poeng = 2;
                tips.ErBeregnet = true;
            }
            else if (TipsOgKampHarSammeVinnerlag(kamp, tips))
            {
                tips.Poeng = 1;
                tips.ErBeregnet = true;
            } else
            {
                tips.Poeng = 0;
                tips.ErBeregnet = true;
            }

        }

        private static bool TipsOgKampHarSammeVinnerlag(Kamp kamp, Tips tips)
        {
            return BådeTipsOgKampErHjemmeseier(kamp, tips) || BådeTipsOgKampErBorteseier(kamp, tips);
        }

        private static bool BådeTipsOgKampErBorteseier(Kamp kamp, Tips tips)
        {
            return (kamp.MålHjemmelag < kamp.MålBortelag && tips.MålHjemmelag < tips.MålBortelag);
        }

        private static bool BådeTipsOgKampErHjemmeseier(Kamp kamp, Tips tips)
        {
            return (kamp.MålHjemmelag > kamp.MålBortelag && tips.MålHjemmelag > tips.MålBortelag);
        }

        private static bool TipsOgKampHarSammeDifferanse(Kamp kamp, Tips tips)
        {
            return (kamp.MålHjemmelag - kamp.MålBortelag) == (tips.MålHjemmelag - tips.MålBortelag);
        }

        private static bool BådeTipsOgKampErUavgjort(Kamp kamp, Tips tips)
        {
            return kamp.MålHjemmelag == kamp.MålBortelag && tips.MålHjemmelag == tips.MålBortelag;
        }

        private static bool TipsErEksaktLiktSomResultat(Kamp kamp, Tips tips)
        {
            return kamp.MålHjemmelag == tips.MålHjemmelag && kamp.MålBortelag == tips.MålBortelag;
        }

        public static void BerengBonusPoeng(Bonus bonus, BonusTips tips)
        {
            if (bonus.Svar == tips.Svar)
            {
                tips.Poeng = 10;
            } else
            {
                tips.Poeng = 0;
            }
            tips.ErBeregnet = true;
        }
    }
}
