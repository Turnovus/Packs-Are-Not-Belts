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
    [StaticConstructorOnStartup]
    public static class PatchRunner
    {
        public static Type gearTabInnerClass = null;
        public static MethodInfo showAsEquipmentMethod = null;
        public static MethodInfo ShowAsApparelMethod = null;

        public static bool GearTabPatchOkay =>
            gearTabInnerClass != null &&
            showAsEquipmentMethod != null &&
            ShowAsApparelMethod != null;

        static PatchRunner()
        {
            gearTabInnerClass = AccessTools.FirstInner(typeof(ITab_Pawn_Gear), t => t.Name == "<>c");

            if (gearTabInnerClass == null)
            {
                Log.Error("Packs are not Beltes: Failed to find inner class if ITab_Pawn_Gear!");
            }
            else
            {
                List<MethodInfo> innerMethods = AccessTools.GetDeclaredMethods(gearTabInnerClass);
                if (innerMethods.Count < 2)
                {
                    Log.Error("Packs are not Belts: Targetted wrong inner class of ITab_Pawn_Gear, aborting patch.");
                }
                else
                {
                    showAsEquipmentMethod = innerMethods[0];
                    ShowAsApparelMethod = innerMethods[1];
                }
            }

            new Harmony("turnovus.submod.packsarenotbelts").PatchAll();
        }
    }
}
