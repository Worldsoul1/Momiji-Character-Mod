using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using Momiji.Source;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core;
using Momiji.Source.StatusEffects;
using LBoL.EntityLib.StatusEffects.Basic;

namespace Momiji.Source.Cards
{
    public sealed class FeedOnTheWeakDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();
            config.Colors = new List<ManaColor>() { ManaColor.Black };
            config.Cost = new ManaGroup() { Any = 2, Black = 1 };
            config.UpgradedCost = new ManaGroup() { Any = 1, Black = 1 };
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Ability;
            config.TargetType = TargetType.Nobody;
            config.Value1 = 4;
            config.UpgradedValue1 = 10;


            config.Illustrator = "";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(FeedOnTheWeakDef))]
    public sealed class FeedOnTheWeak : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            yield return new ApplyStatusEffectAction<FakeOpeningSe>(Battle.Player, 1, 0, 0, 0, 0.2f);
            //This is equivalent to:
            //yield return new ApplyStatusEffectAction<SampleCharacterTurnGainSpiritSe>(Battle.Player, base.Value1, 0, 0, 0, 0.2f);
            yield break;
        }
    }
}