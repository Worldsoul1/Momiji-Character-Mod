using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using Momiji.Source;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.StatusEffects;
using LBoL.EntityLib.StatusEffects.Basic;
using LBoL.Core.Battle.Interactions;
using LBoL.Core.Cards;
using LBoL.EntityLib.Cards.Neutral.NoColor;


namespace Momiji.Source.Cards
{
    public sealed class HuntersTrapDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();

            config.Colors = new List<ManaColor>() { ManaColor.Red };
            config.Cost = new ManaGroup() { Any = 1, Red = 1 };
            config.UpgradedCost = new ManaGroup() { Any = 2 };
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Defense;
            config.TargetType = TargetType.Self;

            config.Block = 12;
            config.UpgradedBlock = 16;

            config.Value1 = 1;
            config.UpgradedValue1 = 1;

            config.RelativeCards = new List<string>() { nameof(AirCutter), nameof(MapleLeaf) };
            config.UpgradedRelativeCards = new List<string>() { nameof(AirCutter), nameof(MapleLeaf) };

            config.Illustrator = "田中 ぬぬ";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(HuntersTrapDef))]
    public sealed class HuntersTrap : SampleCharacterCard
    {
        //By default, if config.Damage / config.Block / config.Shield are set:
        //The card will deal damage or gain Block/Barrier without having to set anything.
        //Here, this is is equivalent to the following code.

            public override Interaction Precondition()
        {
            List<Card> cards = new List<Card>
            {
                Library.CreateCard<AirCutter>(),
                Library.CreateCard<MapleLeaf>()
            };
            return new SelectCardInteraction(1, 1, cards, SelectedCardHandling.DoNothing);
        } 
    
        // Token: 0x060009B4 RID: 2484 RVA: 0x00014436 File Offset: 0x00012636
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            if (precondition == null)
            {
                yield break;
            }
            SelectCardInteraction selectCardInteraction = (SelectCardInteraction)precondition;
            List<Card> cards = new List<Card>
            {
                selectCardInteraction.SelectedCards[0],
            };
            yield return new AddCardsToHandAction(cards, AddCardsType.Normal);
            yield return DefenseAction(true);
            yield break;    
        }
    }
}
