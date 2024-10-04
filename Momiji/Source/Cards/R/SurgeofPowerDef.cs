using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using Momiji.Source;
using Momiji.Source.StatusEffects;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.Battle.BattleActions;
using LBoL.EntityLib.JadeBoxes;

namespace Momiji.Source
{
    public sealed class SurgeofPowerDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();
            config.Colors = new List<ManaColor>() { ManaColor.Red };
            config.Cost = new ManaGroup() { Any = 1, Red = 1 };
            config.UpgradedCost = new ManaGroup() { Any = 1 };
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Skill;
            config.TargetType = TargetType.Nobody;

            config.Value1 = 2;
            config.UpgradedValue1 = 2;

            config.Mana = new ManaGroup() { Any = 0 };
            config.UpgradedMana = new ManaGroup() { Any = 0 };

            config.Illustrator = "";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(SurgeofPowerDef))]
    public sealed class SurgeofPower : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            //Add a token card to the hand.
            if(!base.Battle.Player.HasStatusEffect<SurgeofPowerSe>())
            {
                yield return new ApplyStatusEffectAction<SurgeofPowerSe>(base.Battle.Player, 1, 0, 0, 0, 0.2f);
            }
            yield return new ApplyStatusEffectAction<SurgeofPowerSe>(base.Battle.Player, base.Value1, 0, 0, 0, 0.2f);
            yield break;
        }
    }
}