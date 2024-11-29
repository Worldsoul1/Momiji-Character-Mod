using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.Intentions;
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
using LBoL.Core.StatusEffects;

namespace Momiji.Source.Cards
{
    public sealed class GentlyCirclingLeavesDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();

            config.Colors = new List<ManaColor>() { ManaColor.White, ManaColor.Red };
            config.Cost = new ManaGroup() { White = 1, Red = 1 };
            config.UpgradedCost = new ManaGroup() { Hybrid = 1, HybridColor = 2 };
            config.Rarity = Rarity.Common;

            config.Type = CardType.Skill;
            config.TargetType = TargetType.Self;

            config.Value1 = 3;
            config.UpgradedValue1 = 3;
            config.Value2 = 3;
            config.UpgradedValue2 = 3;
            config.Block = 4;
            config.UpgradedBlock = 4;

            config.RelativeEffects = new List<string>() { nameof(Reflect) };
            config.UpgradedRelativeEffects = new List<string>() { nameof(Reflect) };

            config.Illustrator = "瑠@紅楼夢J-14a";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(GentlyCirclingLeavesDef))]
    public sealed class GentlyCirclingLeaves : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            DrawManyCardAction drawAction = new DrawManyCardAction(base.Value1);
            yield return drawAction;
            IReadOnlyList<Card> drawnCards = drawAction.DrawnCards;
            int num = drawnCards.Count((Card card) => card.CardType == (CardType.Attack));
            int defense = drawnCards.Count((Card card) => card.CardType == CardType.Defense);
            if (num > 0)
            {
                yield return base.BuffAction<Reflect>(base.Value2 * num, 0, 0, 0, 0.2f);
            }
            if (defense > 0)
            {
                yield return base.DefenseAction(base.Block.Block * defense, 0, BlockShieldType.Direct, false);
            }
            yield break;
        }
    }
}