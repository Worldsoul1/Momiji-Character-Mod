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
    public sealed class IndiscriminateRevengeDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();
            config.Colors = new List<ManaColor>() { ManaColor.White, ManaColor.Red };
            config.Cost = new ManaGroup() { White = 2, Red = 2 };
            config.UpgradedCost = new ManaGroup() { White = 2, Red = 2 };
            config.Rarity = Rarity.Rare;

            config.Type = CardType.Ability;
            config.TargetType = TargetType.Self;

            config.Value1 = 7;
            config.UpgradedValue1 = 12;

            config.RelativeEffects = new List<string>() { nameof(RetaliationSe), nameof(Reflect) };
            config.UpgradedRelativeEffects = new List<string>() { nameof(RetaliationSe), nameof(Reflect) };


            config.Illustrator = "";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(IndiscriminateRevengeDef))]
    public sealed class IndiscriminateRevenge : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            yield return new ApplyStatusEffectAction<RetaliationSe>(Battle.Player, base.Value1, 0, 0, 0, 0.2f);
            yield return new ApplyStatusEffectAction<IndiscriminateRevengeSe>(base.Battle.Player, 1, 0, 0, 0, 0.2f);
            //This is equivalent to:
            //yield return new ApplyStatusEffectAction<SampleCharacterTurnGainSpiritSe>(Battle.Player, base.Value1, 0, 0, 0, 0.2f);
            yield break;
        }
    }
}