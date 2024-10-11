using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using LBoL.Base;
using LBoL.Base.Extensions;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Battle.Interactions;
using LBoL.Core.Cards;
using LBoL.Core.StatusEffects;
using LBoLEntitySideloader.Attributes;

namespace Momiji.Source.Cards
{
    public sealed class LongVigilDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();

            config.Colors = new List<ManaColor>() { ManaColor.White };
            config.Cost = new ManaGroup() { Any = 1, White = 1 };
            config.UpgradedCost = new ManaGroup() { Any = 1, White = 1 };
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Skill;
            config.TargetType = TargetType.Self;

            config.Value1 = 3;

            config.Block = 7;
            config.UpgradedBlock = 7;

            config.Keywords = Keyword.Exile;
            config.UpgradedKeywords = Keyword.Exile;

            config.Illustrator = "幻騒アぽろ";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }
    [EntityLogic(typeof(LongVigilDef))]
    public sealed class LongVigil : SampleCharacterCard
    {
        int count = 0;
        // Token: 0x06000941 RID: 2369 RVA: 0x00013B10 File Offset: 0x00011D10
        public override Interaction Precondition()
        {
            if (this.IsUpgraded)
            {
                List<Card> list = (from card in base.Battle.HandZone.Concat(base.Battle.DrawZoneToShow).Concat(base.Battle.DiscardZone)
                                   where card != this
                                   select card).ToList<Card>();
                if (!list.Empty<Card>())
                {
                    return new SelectCardInteraction(0, base.Value1, list, SelectedCardHandling.DoNothing);
                }
                return null;
            }
            else
            {
                List<Card> list2 = (from card in base.Battle.HandZone
                                    where card != this
                                    select card).ToList<Card>();
                if (!list2.Empty<Card>())
                {
                    return new SelectHandInteraction(0, base.Value1, list2);
                }
                return null;
            }
        }

        // Token: 0x06000942 RID: 2370 RVA: 0x00013BB9 File Offset: 0x00011DB9
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            if (precondition != null)
            {
                IReadOnlyList<Card> readOnlyList = this.IsUpgraded ? ((SelectCardInteraction)precondition).SelectedCards : ((SelectHandInteraction)precondition).SelectedCards;
                if (readOnlyList.Count > 0)
                {
                    yield return new ExileManyCardAction(readOnlyList);
                    count = readOnlyList.Count;
                }
            }
            for(int i = 0; i < count; i++) { yield return new CastBlockShieldAction(base.Battle.Player, base.Block.Block, 0, BlockShieldType.Normal, true); }
            yield break;
        }
    }
}
