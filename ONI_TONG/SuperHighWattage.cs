using TUNING;
using UnityEngine;

namespace ONI_TONG
{
    public class SuperHighWattageWireConfig : BaseWireConfig
    {
        public override BuildingDef CreateBuildingDef()
        {
            float[] tieR2 = BUILDINGS.CONSTRUCTION_MASS_KG.TIER2;
            EffectorValues none = NOISE_POLLUTION.NONE;
            EffectorValues tieR5 = BUILDINGS.DECOR.PENALTY.TIER5;
            string id = this.GetId();
            string anim = "utilities_tong_pics_kanim";
            float construction_time = 2f;
            float insulation = 0f;
            BuildingDef buildingDef = base.CreateBuildingDef(id, anim, construction_time, tieR2, insulation,
                tieR5, none);
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