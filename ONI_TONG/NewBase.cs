using System;
using System.Collections.Generic;
using Database;
using Harmony;
using STRINGS;
using UnityEngine;

namespace ONI_TONG
{
    // 将电线的最大电压增大10倍     // 修改电线输出功率
    [HarmonyPatch(typeof(BaseWireConfig))]
    [HarmonyPatch("DoPostConfigureComplete", new Type[] {typeof(Wire.WattageRating), typeof(GameObject)})]
    internal class EnumWire
    {
        static void Postfix(BaseWireConfig __instance, Wire.WattageRating rating, GameObject go)
        {
            Wire component = go.GetComponent<Wire>();
            switch (rating)
            {
                case Wire.WattageRating.Max500:
                    break;
                case Wire.WattageRating.Max1000:
                    break;
                case Wire.WattageRating.Max2000:
                    break;
                case Wire.WattageRating.Max20000:
                    break;
                case Wire.WattageRating.Max50000:
                    break;
                default:
                    rating = Wire.WattageRating.Max50000;
                    break;
            }

            component.MaxWattageRating = rating;
            float maxWattageAsFloat = Wire.GetMaxWattageAsFloat(rating);
            Descriptor item = default(Descriptor);
            item.SetupDescriptor(
                string.Format(UI.BUILDINGEFFECTS.MAX_WATTAGE,
                    GameUtil.GetFormattedWattage(maxWattageAsFloat, GameUtil.WattageFormatterUnit.Automatic)),
                string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.MAX_WATTAGE, new object[0]),
                Descriptor.DescriptorType.Effect);
            Building component2 = go.GetComponent<Building>();
            BuildingDef def = component2.Def;
            if (def.EffectDescription == null)
            {
                def.EffectDescription = new List<Descriptor>();
            }

            def.EffectDescription.Add(item);
        }
    }

    [HarmonyPatch(typeof(Wire), "GetMaxWattageAsFloat", new Type[] {typeof(Wire.WattageRating)})]
    internal class SuperToilet
    {
        static void Postfix(ref float __result, Wire.WattageRating rating)
        {
            switch (rating)
            {
                case Wire.WattageRating.Max500:
                    __result = 500f;
                    break;
                case Wire.WattageRating.Max1000:
                    __result = 1000f;
                    break;
                case Wire.WattageRating.Max2000:
                    __result = 20000f;
                    break;
                case Wire.WattageRating.Max20000:
                    __result = 20000f;
                    break;
                case Wire.WattageRating.Max50000:
                    __result = 100000f;
                    break;
                default:
                    __result = 100000f;
                    break;
            }
        }
    }

    // 新增超级高压线
    [HarmonyPatch(typeof(GeneratedBuildings), "LoadGeneratedBuildings")]
    internal class SuperWire
    {
        static void Prefix()
        {
            AddBuildingStrings("SuperHighWattageWire", "Super High Wattage Wire",
                "that's cool",
                "Golden Legend ");
        }

        static void AddBuildingStrings(string buildingId, string name, string description, string effect)
        {
            Strings.Add(new string[]
            {
                "STRINGS.BUILDINGS.PREFABS." + buildingId.ToUpperInvariant() + ".NAME",
                UI.FormatAsLink(name, buildingId)
            });
            Strings.Add(new string[]
            {
                "STRINGS.BUILDINGS.PREFABS." + buildingId.ToUpperInvariant() + ".DESC",
                description
            });
            ModUtil.AddBuildingToPlanScreen("Power", "SuperHighWattageWire");
        }

        static void AddBuildingToPlanScreen(HashedString category, string buildingId, string addAfterBuildingId = null)
        {
            int num = TUNING.BUILDINGS.PLANORDER.FindIndex((PlanScreen.PlanInfo x) => x.category == category);
            if (num == -1)
            {
                return;
            }

            IList<string> list = TUNING.BUILDINGS.PLANORDER[num].data as IList<string>;
            if (list == null)
            {
                Debug.Log("Could not add " + buildingId + " to the building menu.");
                return;
            }

            int num2 = list.IndexOf(addAfterBuildingId);
            if (num2 != -1)
            {
                list.Insert(num2 + 1, buildingId);
                return;
            }

            list.Add(buildingId);
        }
    }

    [HarmonyPatch(typeof(Database.Techs))]
    [HarmonyPatch("Init")]
    internal class DbInitializePatch
    {
        // Token: 0x06000047 RID: 71 RVA: 0x00003480 File Offset: 0x00001680
        [HarmonyPostfix]
        public static void Postfix()
        {
            Db.Get().Techs.Get("AdvancedPowerRegulation").unlockedItemIDs.Add("SuperHighWattageWire");
        }
    }
}