using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace PacksAreNotBelts
{
    class PacksAreNotBeltsSettings : ModSettings
    {
        public bool nudistsIgnoreUtility = true;
        public bool utilityIgnoresDamage = true;
        public bool useLegacySmoke = false;
        public bool useArtifactLayer = false;
        public bool useAmmoLayerBandolier = false;
        public bool useMechLayer = false;
        public bool useTacticalLayer = false;
        public bool useTacticalLayerEft = false;
        public bool useTacticalLayerEftArmor = false;
        public bool useAmmoLayer = false;
        public bool useAmmoLayerAccessories = false;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref nudistsIgnoreUtility, "nudistsIgnoreUtility");
            Scribe_Values.Look(ref utilityIgnoresDamage, "utilityIgnoresDamage");
            Scribe_Values.Look(ref useLegacySmoke, "useLegacySmoke");
            Scribe_Values.Look(ref useArtifactLayer, "useArtifactLayer");
            Scribe_Values.Look(ref useAmmoLayerBandolier, "useAmmoLayerBandolier");
            Scribe_Values.Look(ref useMechLayer, "useMechLayer");
            Scribe_Values.Look(ref useTacticalLayer, "useTacticalLayer");
            Scribe_Values.Look(ref useTacticalLayerEft, "useTacticalLayerEft");
            Scribe_Values.Look(ref useTacticalLayerEftArmor, "useTacticalLayerEftArmor");
            Scribe_Values.Look(ref useAmmoLayer, "useAmmoLayer");
            Scribe_Values.Look(ref useAmmoLayerAccessories, "useAmmoLayerAccessories");
        }
    }
}
