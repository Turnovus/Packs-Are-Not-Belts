using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;
using UnityEngine;

namespace PacksAreNotBelts
{
    class PacksAreNotBeltsMod : Mod
    {
        public const string PackageId = "Turnovus.RimWorld.PacksAreNotBelts.Test";
        public const string KeyPrefix = "PacksAreNotBelts.";
        public const string TitlePostfix = ".Title";
        public const string DescriptionPostfix = ".Description";

        public PacksAreNotBeltsSettings settings;

        public PacksAreNotBeltsMod(ModContentPack content) : base(content)
        {
            settings = GetSettings<PacksAreNotBeltsSettings>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listing = new Listing_Standard();
            listing.Begin(inRect);

            DoSettingListing(listing, "NudistsIgnore", ref settings.nudistsIgnoreUtility);
            DoSettingListing(listing, "NoDamageUtilities", ref settings.utilityIgnoresDamage);
            DoSettingListing(listing, "LegacySmokepop", ref settings.useLegacySmoke);
            DoSettingListing(listing, "ArtifactLayer", ref settings.useArtifactLayer);
            DoSettingListing(listing, "Bandolier", ref settings.useAmmoLayerBandolier);
            DoSettingListing(listing, "Mechanitor", ref settings.useMechLayer);
            DoSettingListing(listing, "RimmunationTwo", ref settings.useTacticalLayer);
            DoSettingListing(listing, "EftTactical", ref settings.useTacticalLayerEft);
            DoSettingListing(listing, "EftArmor", ref settings.useTacticalLayerEftArmor);
            DoSettingListing(listing, "RimEffectAmmo", ref settings.useAmmoLayer);
            DoSettingListing(listing, "AccessoriesExpanded", ref settings.useAmmoLayerAccessories);

            listing.End();
        }

        public static void DoSettingListing(Listing_Standard list, string key, ref bool setting)
        {
            string t = KeyPrefix + key + TitlePostfix;
            string d = KeyPrefix + key + DescriptionPostfix;
            list.CheckboxLabeled(t.Translate(), ref setting, d.Translate());
        }

        public override string SettingsCategory()
        {
            return "Packs Are Not Belts";
        }
    }
}
