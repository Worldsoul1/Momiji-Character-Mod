using LBoL.Base;
using LBoL.Base.Extensions;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using Momiji.Source;
using Momiji.Source.GunName;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.StatusEffects;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using System.Linq;
using LBoL.Core.Units;
using LBoL.EntityLib.Cards.Character.Sakuya;

namespace Momiji.Source
{
    public sealed class TrainingRecordDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();
            config.GunName = GunNameID.GetGunFromId(400);
            config.FindInBattle = false;
            config.ToolPlayableTimes = 4;

            config.Colors = new List<ManaColor>() { ManaColor.White, ManaColor.Red };
            config.Cost = new ManaGroup() { Any = 1, White = 1, Red = 1 };
            config.UpgradedCost = new ManaGroup() { Any = 2, Hybrid = 1, HybridColor = 2 };
            config.Rarity = Rarity.Rare;

            config.Type = CardType.Attack;
            //TargetType.AllEnemies will change the selector to all enemies for attacks/status effects.
            config.TargetType = TargetType.SingleEnemy;

            config.Damage = 10;
            config.UpgradedDamage = 15;

            config.Value1 = 2;
            config.UpgradedValue1 = 3;
            config.Value2 = 1;
            config.UpgradedValue2 = 2;

            config.Keywords = Keyword.Exile;
            config.UpgradedKeywords = Keyword.Exile;

            //Add Lock On descrption when hovering over the card.


            config.Illustrator = "";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(TrainingRecordDef))]
    public sealed class TrainingRecord : SampleCharacterCard
    {
        private bool Active
        {
            get
            {
                if (base.Battle != null)
                {
                    return !base.Battle.BattleCardUsageHistory.Any((Card card) => card is TimeWalk);
                }
                return true;
            }
        }
        int intention = 0;
        private static bool CheckForCardTypeAndIntention(Card card, int intention)
        {
            if(card.CardType == CardType.Attack && (intention == 1 || intention == 3 || intention == 5 || intention == 7))
                return true;
            if(card.CardType == CardType.Defense && (intention == 2 || intention == 3 || intention == 6 || intention == 7))
                return true;
            if((card.CardType == CardType.Ability || card.CardType == CardType.Skill) && (intention >= 4))
                return true;
            return false;
            
        }

        // Token: 0x060009CD RID: 2509 RVA: 0x000145D3 File Offset: 0x000127D3
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            if (this.Active)
            {
                Card cardToUpgrade = null;
                EnemyUnit selectedEnemy = selector.SelectedEnemy;
                intention = base.IntentionCheck(selectedEnemy);
                IEnumerable<Card> eligibleCards = base.GameRun.BaseDeck.Where(c => c.CanUpgradeAndPositive).Where(c => CheckForCardTypeAndIntention(c, intention));

                if (eligibleCards.Count() > 0)
                {
                    cardToUpgrade = eligibleCards.Sample(base.GameRun.GameRunEventRng);
                    // the rest of the fucking owl

                }
                Card cardInDeckToUpgrade = cardToUpgrade;
                if (cardInDeckToUpgrade != null)
                {
                    base.GameRun.UpgradeDeckCard(cardInDeckToUpgrade, false);
                }

                Card cardInBattleToUpgrade = base.Battle.EnumerateAllCards().Where(c => c.CanUpgrade && c.InstanceId == cardInDeckToUpgrade.InstanceId).FirstOrDefault();
                if (null != cardInBattleToUpgrade)
                {
                    yield return new UpgradeCardAction(cardInBattleToUpgrade);
                }
            }
            yield return base.AttackAction(selector, GunName);
        }
        // Token: 0x06000B44 RID: 2884 RVA: 0x0001638B File Offset: 0x0001458B        
    }
}


