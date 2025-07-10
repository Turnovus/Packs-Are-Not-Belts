using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;
using HarmonyLib;
using System.Reflection;

namespace PacksAreNotBelts
{
    [HarmonyPatch(typeof(ApparelLayerDef))]
    [HarmonyPatch(nameof(ApparelLayerDef.IsUtilityLayer))]
    [HarmonyPatch(MethodType.Getter)]
    class ForceUtilityLayerIfExtension
    {
        public static void Postfix(ApparelLayerDef __instance, ref bool __result)
        {
            if (__instance.HasModExtension<ModExtension_IsUtilityLayer>())
            {
                __result = true;
            }
        }
    }

    [HarmonyPatch]
    class ShowAsEquipmentPatch
    {
        public static bool Prepare => PatchRunner.GearTabPatchOkay;

        public static MethodBase TargetMethod()
        {
            return PatchRunner.showAsEquipmentMethod;
        }

        public static void Postfix(Apparel x, ref bool __result)
        {
            if (__result == false)
                __result = x.def.apparel?.layers?.Any(l => l.HasModExtension<ModExtension_IsUtilityLayer>()) == true;
        }
    }

    [HarmonyPatch]
    class ShowAsApparelPatch
    {
        public static bool Prepare => PatchRunner.GearTabPatchOkay;

        public static MethodBase TargetMethod()
        {
            return PatchRunner.ShowAsApparelMethod;
        }

        public static void Postfix(Apparel x, ref bool __result)
        {
            if (__result == true)
                __result = !(__result = x.def.apparel?.layers?.Any(l => l.HasModExtension<ModExtension_IsUtilityLayer>()) == true);
        }
    }
}
