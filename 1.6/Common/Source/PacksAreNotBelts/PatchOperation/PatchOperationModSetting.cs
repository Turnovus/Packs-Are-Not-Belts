using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Verse;
using RimWorld;
using System.Xml;

namespace PacksAreNotBelts
{
    class PatchOperationModSetting : PatchOperation
    {
        private bool invert = false;
        private string setting;
        private PatchOperation operation;

        protected override bool ApplyWorker(XmlDocument xml)
        {
            PacksAreNotBeltsSettings settings = LoadedModManager.GetMod<PacksAreNotBeltsMod>().GetSettings<PacksAreNotBeltsSettings>();
            FieldInfo field = typeof(PacksAreNotBeltsSettings).GetField(setting, BindingFlags.Public | BindingFlags.Instance);
            if (field == null || field.FieldType != typeof(bool))
                return false;

            bool b = (bool)field.GetValue(settings);
            if (invert)
                b = !b;

            if (b)
                return operation.Apply(xml);
            return true;
        }
    }
}
