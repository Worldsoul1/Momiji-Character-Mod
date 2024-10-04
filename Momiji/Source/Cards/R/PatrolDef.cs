using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.StatusEffects;
using LBoL.EntityLib.StatusEffects.Basic;
using LBoLEntitySideloader.Attributes;
using System;
using LBoL.Core.Battle.BattleActions;
using System.Collections.Generic;
using System.Text;
using LBoL.Core.Units;
using Momiji.Source.StatusEffects;
using System.Linq;
using LBoL.Core.Cards;

namespace Momiji.Source.Cards
{
    public sealed class PatrolDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();

            config.Colors = new List<ManaColor>() { ManaColor.Red};
            config.Cost = new ManaGroup() { Any = 1, Red = 1};
            config.UpgradedCost = new ManaGroup() { Any = 1, Red = 1};
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Skill;
            config.TargetType = TargetType.Self;

            config.Value1 = 3;
            config.UpgradedValue1 = 4;
            config.Value2 = 1;
            config.UpgradedValue2 = 1;

            config.RelativeEffects = new List<string>() { nameof(Graze) };
            config.UpgradedRelativeEffects = new List<string>() { nameof(Graze) };

            config.Illustrator = "耳总明岚";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(PatrolDef))]
    public sealed class Patrol : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            DrawManyCardAction drawAction = new DrawManyCardAction(base.Value1);
            yield return drawAction;
            IReadOnlyList<Card> drawnCards = drawAction.DrawnCards;
            int num = drawnCards.Count((Card card) => card.Config.Colors.Contains(ManaColor.Red));
            if(this.IsUpgraded)
            {
                num += drawnCards.Count((Card card) => (card.CardType == CardType.Status || card.CardType == CardType.Misfortune));
            }
            if (num > 0)
            {
                yield return base.BuffAction<Graze>(base.Value2 * num, 0, 0, 0, 0.2f);
            }
            yield break;
        }
    }
}