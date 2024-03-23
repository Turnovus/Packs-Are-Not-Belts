using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;
using HarmonyLib;

namespace PacksAreNotBelts
{
    [HarmonyPatch(typeof(ThoughtWorker_NudistNude))]
    [HarmonyPatch("CurrentStateInternal")]
    class NudistNudePatch
    {

        static void Postfix(Pawn p, ref ThoughtState __result)
        {
            bool nudistsIgnoreUtility = LoadedModManager.GetMod<PacksAreNotBeltsMod>().settings.nudistsIgnoreUtility;
            if (__result.Active || !nudistsIgnoreUtility)
                return;
            List<Apparel> wornApparel = p.apparel.WornApparel;
            bool noUtilities = true;
            foreach (Apparel apparel in wornApparel)
            {
                foreach (BodyPartGroupDef bodyPart in apparel.def.apparel.bodyPartGroups)
                {
                    if (bodyPart == BodyPartGroupDefOf.Torso || bodyPart == BodyPartGroupDefOf.Legs)
                    {
                        if (!apparel.def.apparel.layers.Contains(ApparelLayerDefOf.Belt) &
                            !apparel.def.apparel.layers.Contains(MyDefOf.PacksAreNotBelts_Tactical))
                            noUtilities = false;
                    }
                }
            }
            if (noUtilities)
                __result = true;
        }

    }
}
