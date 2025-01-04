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
    public sealed class ImageTrainingDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();
            config.Colors = new List<ManaColor>() { ManaColor.Red };
            config.Cost = new ManaGroup() { Any = 2, Red = 1};
            config.UpgradedCost = new ManaGroup() { Any = 1, Red = 1 };
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Ability;
            config.TargetType = TargetType.Nobody;

            config.Value1 = 1;
            config.UpgradedValue1 = 2;

            config.Value2 = 1;
            config.UpgradedValue2 = 2;

            config.RelativeCards = new List<string>() { "AirCutter" };
            config.UpgradedRelativeCards = new List<string>() { "AirCutter" };

            config.Illustrator = "きゃなが";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(ImageTrainingDef))]
    public sealed class ImageTraining : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            yield return new AddCardsToHandAction(Library.CreateCards<AirCutter>(base.Value1, false), AddCardsType.Normal);
            yield return base.BuffAction<ImageTrainingSe>(1, 0, 0, 0, 0.2f);
            //This is equivalent to:
            //yield return new ApplyStatusEffectAction<Firepower>(Battle.Player, base.Value1, 0, 0, 0, 0.2f);
            //yield return new ApplyStatusEffectAction<Spirit>(Battle.Player, base.Value2, 0, 0, 0, 0.2f);
            yield break;
        }
    }
}