using TUNING;
using UnityEngine;

namespace ONI_TONG
{
    public class SuperHighWattageWireConfig :BaseWireConfig
    {
        public override BuildingDef CreateBuildingDef()
        {
            string id = this.GetId();
            string anim = "utilities_tong_pics_kanim";
            float construction_time = 2f;
            float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER2;
            float insulation = 0f;
            EffectorValues none = NOISE_POLLUTION.NONE;
            BuildingDef buildingDef = base.CreateBuildingDef(id, anim, construction_time, tier, insulation, BUILDINGS.DECOR.PENALTY.TIER5, none);
            return buildingDef;
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
            base.DoPostConfigureComplete(Wire.WattageRating.NumRatings, go);
        }
        public const string Id = "SuperHighWattageWire";
        public const string DisplayName = "Super High Wattage Wire";
        public const string Description = "Golden Legend";
        protected virtual string GetId()
        {
            return "SuperHighWattageWire";
        }
    }
}