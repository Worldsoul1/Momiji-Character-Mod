using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using Momiji.Source;
using Momiji.Source.GunName;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.StatusEffects;
using LBoL.Core.Cards;
using System.Linq;
using LBoL.Core.Battle.Interactions;
using Momiji.Source.BattleActions;
using LBoL.EntityLib.StatusEffects.Basic;
using LBoL.Core.Battle.BattleActions;

namespace Momiji.Source.Cards
{
    public sealed class CallToArmsDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();

            config.Colors = new List<ManaColor>() { ManaColor.White };
            config.Cost = new ManaGroup() { Any = 1, White = 1 };
            config.UpgradedCost = new ManaGroup() { White = 1 };
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Skill;
            config.TargetType = TargetType.Nobody;

            config.Value1 = 1;

            config.Keywords = Keyword.Exile;
            config.UpgradedKeywords = Keyword.Exile;

            config.Illustrator = "腹パンくん";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(CallToArmsDef))]
    public sealed class CallToArms : SampleCharacterCard
    {
        public override Interaction Precondition()
        {
            IReadOnlyList<Card> drawZoneToShow = base.Battle.DrawZoneToShow;
            if (drawZoneToShow.Count <= 0)
            {
                return null;
            }
            return new SelectCardInteraction(0, base.Value1, drawZoneToShow, SelectedCardHandling.DoNothing);
        }

        // Token: 0x06000EE8 RID: 3816 RVA: 0x0001A982 File Offset: 0x00018B82
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            SelectCardInteraction selectCardInteraction = (SelectCardInteraction)precondition;
            IReadOnlyList<Card> readOnlyList = (selectCardInteraction != null) ? selectCardInteraction.SelectedCards : null;
            if (readOnlyList != null && readOnlyList.Count > 0)
            {
                foreach (Card card in readOnlyList)
                {
                    yield return new MoveCardAction(card, CardZone.Hand);
                    if(!card.IsUpgraded)
                    {
                        yield return new UpgradeCardAction(card);
                    }
                }
                IEnumerator<Card> enumerator = null;
            }
            yield break;
        }
    }
}