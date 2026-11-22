using HarmonyLib;
using RimWorld;
using Verse;

namespace PacksAreNotBelts
{
    [HarmonyPatch(typeof(Pawn_ApparelTracker))]
    [HarmonyPatch("TakeWearoutDamageForDay")]
    public class UtilityDegradationPatch
    {
        [HarmonyPrefix]
        public static bool IgnoreUtilities(Thing ap)
        {
            if (!LoadedModManager.GetMod<PacksAreNotBeltsMod>().settings.utilityDoesNotDegrade)
                return true;

            if (ap is Apparel apparel && ArmorPatch.IsUtility(apparel))
                return false;
            
            return true;
        }
    }
}