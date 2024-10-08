using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using Momiji.Source;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core;
using LBoL.Core.StatusEffects;
using Momiji.Source.StatusEffects;
using LBoL.EntityLib.StatusEffects.Basic;

namespace Momiji.Source.Cards
{
    public sealed class ShieldCounterDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();
            config.Colors = new List<ManaColor>() { ManaColor.Red };
            config.Cost = new ManaGroup() { Any = 1, Red = 2 };
            config.UpgradedCost = new ManaGroup() { Red = 1 };
            config.Rarity = Rarity.Rare;

            config.Type = CardType.Ability;
            config.TargetType = TargetType.Nobody;
            config.Value1 = 2;
            config.UpgradedValue1 = 2;
            config.Value2 = 10;
            config.UpgradedValue2 = 15;

            config.RelativeEffects = new List<string>() { nameof(Reflect), nameof(Vulnerable) };
            config.UpgradedRelativeEffects = new List<string>() { nameof(Reflect), nameof(Vulnerable) };

            config.Illustrator = "茉屑";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(ShieldCounterDef))]
    public sealed class ShieldCounter : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            yield return new ApplyStatusEffectAction<ShieldCounterSe>(Battle.Player, base.Value1, 0, 0, 0, 0.2f);
            yield return new ApplyStatusEffectAction<Reflect>(base.Battle.Player, base.Value2, 0, 0, 0, 0.2f);
            //This is equivalent to:
            //yield return new ApplyStatusEffectAction<SampleCharacterTurnGainSpiritSe>(Battle.Player, base.Value1, 0, 0, 0, 0.2f);
            yield break;
        }
    }
}