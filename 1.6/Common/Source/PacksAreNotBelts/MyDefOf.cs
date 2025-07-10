using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;

namespace PacksAreNotBelts
{
    [DefOf]
    public static class MyDefOf
    {
        public static ApparelLayerDef PacksAreNotBelts_Tactical;

        static MyDefOf() =>
            DefOfHelper.EnsureInitializedInCtor(typeof(MyDefOf));
    }
}
