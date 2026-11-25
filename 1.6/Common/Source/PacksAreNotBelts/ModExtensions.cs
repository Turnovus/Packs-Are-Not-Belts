using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;

namespace PacksAreNotBelts
{
    class ApparelLayerExtension : DefModExtension
    {
        public bool disableDamage = true;
        public bool disableDeterioration = true;
        public bool isEquipment = true;
        public bool isUtilityLayer = false;
    }
}
