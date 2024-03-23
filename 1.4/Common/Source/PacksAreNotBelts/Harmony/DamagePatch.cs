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
    [HarmonyPatch(typeof(DamageWorker))]
    [HarmonyPatch("Apply")]
    class DamagePatch
    {
        public static bool IsUtility(Apparel a)
        {
            return a.def.apparel.layers.Where(l => l.IsUtilityLayer).Any();
        }

        static bool Prefix(ref DamageWorker.DamageResult __result, Thing victim)
        {
            bool utilityIgnoresDamage = LoadedModManager.GetMod<PacksAreNotBeltsMod>().settings.utilityIgnoresDamage;
            if (utilityIgnoresDamage && victim is Apparel a && a.Wearer != null && IsUtility(a))
            {
                DamageWorker.DamageResult damageResult = new DamageWorker.DamageResult();
                damageResult.totalDamageDealt = 0.0f;
                __result = damageResult;
                return false;
            }
            return true;
        }
    }
}
