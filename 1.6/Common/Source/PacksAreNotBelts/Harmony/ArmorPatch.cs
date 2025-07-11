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
    [HarmonyPatch(typeof(ArmorUtility))]
    [HarmonyPatch("ApplyArmor")]
    public static class ArmorPatch
    {
        public static bool IsUtility(Apparel a)
        {
            List<ApparelLayerDef> layers = a?.def.apparel?.layers;
            if (layers.NullOrEmpty())
                return false;

            foreach (ApparelLayerDef layer in layers)
                if (layer.IsUtilityLayer)
                    return true;

            return false;
        }

        [HarmonyPrefix]
        public static bool IgnoreUtilities(Thing armorThing)
        {
            if (!LoadedModManager.GetMod<PacksAreNotBeltsMod>().settings.utilityIgnoresDamage)
                return true;

            if (IsUtility(armorThing as Apparel))
                return false;

            return true;
        }
    }
}
