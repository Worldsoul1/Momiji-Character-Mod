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

namespace Momiji.Source.Cards.W
{
    public sealed class TirelessGuardDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();
            config.Colors = new List<ManaColor>() { ManaColor.White };
            config.Cost = new ManaGroup() { Any = 1, White = 3 };
            config.UpgradedCost = new ManaGroup() { Any = 2, White = 2 };
            config.Rarity = Rarity.Rare;

            config.Type = CardType.Ability;
            config.TargetType = TargetType.Nobody;

            config.Value1 = 1;
            config.UpgradedValue1 = 1;

            config.RelativeEffects = new List<string>() { nameof(Reflect) };
            config.UpgradedRelativeEffects = new List<string>() { nameof(Reflect) };

            config.Illustrator = "Oba";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(TirelessGuardDef))]
    public sealed class TirelessGuard : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            yield return new ApplyStatusEffectAction<TirelessGuardSe>(Battle.Player, base.Value1, 0, 0, 0, 0.2f);
            //This is equivalent to:
            //yield return new ApplyStatusEffectAction<SampleCharacterTurnGainSpiritSe>(Battle.Player, base.Value1, 0, 0, 0, 0.2f);
            yield break;
        }
    }
}