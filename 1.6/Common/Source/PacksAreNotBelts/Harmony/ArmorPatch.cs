using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;
using HarmonyLib;

namespace PacksAreNotBelts
{
    // NOTE: Combat Extended seems to override armor application, so this patch doesn't do anything
    // if CE is installed.
    [HarmonyPatch(typeof(ArmorUtility))]
    [HarmonyPatch("ApplyArmor")]
    public static class ArmorPatch
    {
        public static readonly MethodInfo M_IsDamageDisabled =
            AccessTools.Method(typeof(ArmorPatch), nameof(ArmorPatch.IsDamageDisabled));

        public static readonly MethodInfo M_RaceProperties_getIsMechanoid =
            AccessTools.PropertyGetter(typeof(RaceProperties), nameof(RaceProperties.IsMechanoid));
        
        public static bool IsDamageDisabled(Thing a)
        {
            if (!LoadedModManager.GetMod<PacksAreNotBeltsMod>().settings.utilityIgnoresDamage)
                return false;
            
            List<ApparelLayerDef> layers = a?.def.apparel?.layers;
            if (layers.NullOrEmpty())
                return false;

            foreach (ApparelLayerDef layer in layers)
                if (layer.GetModExtension<ApparelLayerExtension>()?.disableDamage == true)
                    return true;

            return false;
        }

        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> AddDamageCondition(IEnumerable<CodeInstruction> instructions)
        {
            // 0 - Looking for RaceProperties.IsMechanoid to know where to inject
            // 1 - Looking for brfalse.s, which is the "if" condition for armor damage
            // 2 - Patch done
            int flag = 0;

            foreach (CodeInstruction instruction in instructions)
            {
                yield return instruction;

                if (flag == 0 && instruction.operand as MethodInfo == M_RaceProperties_getIsMechanoid)
                {
                    flag++;
                }
                else if (flag == 1 && instruction.opcode == OpCodes.Brfalse_S)
                {
                    Label? label = instruction.operand as Label?;
                    if (label == null)
                        continue;

                    yield return new CodeInstruction(OpCodes.Ldarg_3);
                    yield return new CodeInstruction(OpCodes.Call, M_IsDamageDisabled);
                    yield return new CodeInstruction(OpCodes.Brtrue_S, label.Value);
                    flag++;
                }
            }
            
            if (flag < 2)
                Log.Error("Packs Are Not Belts: ArmorPatch.AddDamageCondition failed! Flag: " + flag);
        }
    }
}
