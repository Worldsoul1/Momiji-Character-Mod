using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using Momiji.Source;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core;
using Momiji.Source.StatusEffects;

namespace Momiji.Source.Cards.W
{
    public sealed class TrapStanceDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();
            config.Colors = new List<ManaColor>() { ManaColor.White };
            config.Cost = new ManaGroup() { White = 3 };
            config.UpgradedCost = new ManaGroup() { White = 2 };
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Ability;
            config.TargetType = TargetType.Nobody;


            config.Value1 = 4;
            config.UpgradedValue1 = 8;
            config.Value2 = 1;
            config.UpgradedValue2 = 1;

            config.Illustrator = "Fall 5754";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(TrapStanceDef))]
    public sealed class TrapStance : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            yield return new ApplyStatusEffectAction<RetaliationSe>(Battle.Player, base.Value1, 0, 0, 0, 0.2f);
            yield return new ApplyStatusEffectAction<TrapStanceSe>(Battle.Player, base.Value2, 0, 0, 0, 0.2f);
            //This is equivalent to:
            //yield return new ApplyStatusEffectAction<SampleCharacterTurnGainSpiritSe>(Battle.Player, base.Value1, 0, 0, 0, 0.2f);
            yield break;
        }
    }
}