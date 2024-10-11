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
            config.Value2 = 4;
            config.UpgradedValue2 = 4;

            config.RelativeEffects = new List<string>() { nameof(RetaliationSe) };
            config.UpgradedRelativeEffects = new List<string>() { nameof(RetaliationSe) };

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
            List <Card> AttackOrSkill = null; 
            DrawManyCardAction drawAction = new DrawManyCardAction(base.Value1);
            yield return drawAction;
            IReadOnlyList<Card> drawnCards = drawAction.DrawnCards;
            if (drawnCards != null && drawnCards.Count > 0)
            {
                AttackOrSkill = (from card in drawnCards
                            where (card.CardType == CardType.Skill || card.CardType == CardType.Attack)
                            select card).ToList<Card>();
            }
            foreach (Card card in AttackOrSkill)
            {
                yield return new ApplyStatusEffectAction<RetaliationSe>(Battle.Player, base.Value2, 0, 0, 0, 0.2f);
            }
            //This is equivalent to:
            //yield return new CastBlockShieldAction(Battle.Player, base.Block, base.Shield, BlockShieldType.Normal, true); 
            yield break;
        }
    }
}