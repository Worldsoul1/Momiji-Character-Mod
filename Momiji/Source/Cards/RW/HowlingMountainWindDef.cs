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
    public sealed class HowlingMountainWindDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();
            config.Colors = new List<ManaColor>() { ManaColor.Red };
            config.Cost = new ManaGroup() { Any = 3, Red = 2 };
            config.UpgradedCost = new ManaGroup() { Any = 2, Red = 1 };
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Ability;
            config.TargetType = TargetType.Nobody;

            config.Value1 = 3;
            config.Value2 = 1;
            config.UpgradedValue2 = 2;

            config.RelativeEffects = new List<string>() { nameof(Vulnerable) };
            config.UpgradedRelativeEffects = new List<string>() { nameof(Vulnerable) };

            config.RelativeCards = new List<string>() { nameof(AirCutter) };
            config.UpgradedRelativeCards = new List<string>() { nameof(AirCutter) };

            config.Illustrator = "らさいと";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(HowlingMountainWindDef))]
    public sealed class HowlingMountainWind : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            yield return new AddCardsToHandAction((Library.CreateCards<AirCutter>(Value2, false)));
            yield return new ApplyStatusEffectAction<HowlingMountainWindSe>(Battle.Player, 1, 0, 0, 0, 0.2f);
            //This is equivalent to:
            //yield return new ApplyStatusEffectAction<Firepower>(Battle.Player, base.Value1, 0, 0, 0, 0.2f);
            //yield return new ApplyStatusEffectAction<Spirit>(Battle.Player, base.Value2, 0, 0, 0, 0.2f);
            yield break;
        }
    }
}
