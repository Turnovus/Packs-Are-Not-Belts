using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using Verse;

namespace PacksAreNotBelts
{
    [HarmonyPatch(typeof(Pawn_ApparelTracker))]
    [HarmonyPatch("TakeWearoutDamageForDay")]
    public class UtilityDegradationPatch
    {
        public static bool IsDeteriorationDisabled(Apparel a)
        {
            List<ApparelLayerDef> layers = a?.def.apparel?.layers;
            if (layers.NullOrEmpty())
                return false;

            foreach (ApparelLayerDef layer in layers)
                if (layer.GetModExtension<ApparelLayerExtension>()?.disableDeterioration == true)
                    return true;

            return false;
        }
        
        [HarmonyPrefix]
        public static bool IgnoreUtilities(Thing ap)
        {
            if (!LoadedModManager.GetMod<PacksAreNotBeltsMod>().settings.utilityDoesNotDegrade)
                return true;

            if (ap is Apparel apparel && IsDeteriorationDisabled(apparel))
                return false;
            
            return true;
        }
    }
}