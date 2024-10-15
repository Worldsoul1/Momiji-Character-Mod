using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using Momiji.Source;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.StatusEffects;
using Momiji.Source.StatusEffects;
using LBoL.Core.Battle.BattleActions;

namespace Momiji.Source.Cards
{
    public sealed class MountainsideTrailTrackingDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();
            config.Colors = new List<ManaColor>() { ManaColor.Red };
            config.Cost = new ManaGroup() { Any = 1, Red = 1 };
            config.UpgradedCost = new ManaGroup() { Any = 1, Red = 1 };
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Ability;
            config.TargetType = TargetType.Nobody;

            config.Value1 = 1;
            config.UpgradedValue1 = 2;

            config.Illustrator = "しなぷう";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(MountainsideTrailTrackingDef))]
    public sealed class MountainsideTrailTracking : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            yield return base.BuffAction<MountainsideTrailTrackingSe>(base.Value1, 0, 0, 0, 0.2f);
            //This is equivalent to:
            //yield return new ApplyStatusEffectAction<Firepower>(Battle.Player, base.Value1, 0, 0, 0, 0.2f);
            //yield return new ApplyStatusEffectAction<Spirit>(Battle.Player, base.Value2, 0, 0, 0, 0.2f);
            yield break;
        }
    }
}