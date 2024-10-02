using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Battle.Interactions;
using LBoL.Core.Cards;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using LBoL.Core.StatusEffects;
using Momiji.Source;
using LBoL.EntityLib.StatusEffects.Enemy;

namespace Momiji.Source.Cards
{
    public sealed class AyaTeammateDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();
            config.Colors = new List<ManaColor>() { ManaColor.Red, ManaColor.Green };
            config.Cost = new ManaGroup() { Any = 1, Red = 1, Green = 1 };
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Friend;
            config.TargetType = TargetType.Nobody;

            //Loyalty is called "Unity" ingame.
            config.Loyalty = 2;
            config.UpgradedLoyalty = 2;
            //Passive cost is the passive amount of Unity gained/consumed at the strt of each turn.  
            config.PassiveCost = 1;
            config.UpgradedPassiveCost = 1;
            //Cost of the Active ability. 
            config.ActiveCost = -3;
            config.UpgradedActiveCost = -3;
            //Cost of the Ultimate ability.
            config.UltimateCost = -6;
            config.UpgradedUltimateCost = -6;

            config.Value1 = 1;
            config.UpgradedValue1 = 2;
            config.Value2 = 3;
            config.UpgradedValue2 = 4;
            
            config.Illustrator = "";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            
            return config;
        }
    }

    [EntityLogic(typeof(AyaTeammateDef))]
    public sealed class AyaTeammate : SampleCharacterCard
    {
        public int Graze
        {
            get
            {
                if (!this.IsUpgraded)
                {
                    return 2;
                }
                return 3;
            }
        }
        public string Indent {get;} = "<indent=80>";
        public string PassiveCostIcon
        {
            get
            {
                return string.Format("<indent=0><sprite=\"Passive\" name=\"{0}\">{1}", base.PassiveCost, Indent);
            }
        }
        public string ActiveCostIcon
        {
            get
            {
                return string.Format("<indent=0><sprite=\"Active\" name=\"{0}\">{1}", base.ActiveCost, Indent);
            }
        }
        public string UltimateCostIcon
        {
            get
            {
                return string.Format("<indent=0><sprite=\"Ultimate\" name=\"{0}\">{1}", base.UltimateCost, Indent);
            }
        }

        //Effect to trigger at the start of the end.
        public override IEnumerable<BattleAction> OnTurnStartedInHand()
		{
			return this.GetPassiveActions();
		}

        public override IEnumerable<BattleAction> GetPassiveActions()
		{
            //Triigger the effect only if the card has been summoned. 
			if (!base.Summoned || base.Battle.BattleShouldEnd)
			{
                yield break;
			}
			base.NotifyActivating();
            //Increase base loyalty.
			base.Loyalty += base.PassiveCost;
			int num;
            //Trigger the action multiple times if "Mental Energy Injection" is active.
			for (int i = 0; i < base.Battle.FriendPassiveTimes; i = num + 1)
			{
				if (base.Battle.BattleShouldEnd)
				{
					yield break;
				}
                if (base.Battle.Player.HasStatusEffect<Graze>())
                {
                    yield return base.BuffAction<Graze>(1, 0, 0, 0, 0.2f);
                }
				num = i;
			}
			yield break;
		}

        public override IEnumerable<BattleAction> SummonActions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            yield return base.BuffAction<Graze>(this.Graze, 0, 0, 0, 0.2f);
            foreach (BattleAction battleAction in base.SummonActions(selector, consumingMana, precondition))
            {
                yield return battleAction;
            }
            IEnumerator<BattleAction> enumerator = null;
            yield break;
        }

        //When the summoned card is played, choose and resolve either the active or ultimate effect.
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
		{
            //
			if (precondition == null || ((MiniSelectCardInteraction)precondition).SelectedCard.FriendToken == FriendToken.Active)
			{
                //Adjust the card's loyalty. 
                //Because the ActiveCost is negative, the Cost has to be added instead of substracted.
				base.Loyalty += base.ActiveCost;
                foreach (BattleAction battleAction in base.DebuffAction<LockedOn>(base.Battle.AllAliveEnemies, base.Value2, 0, 0, 0, true, 0.2f))
                {
                    yield return battleAction;
                }
                yield return new AddCardsToHandAction(Library.CreateCards<MapleLeaf>(Value1, false));
                yield return base.SkillAnime;
			}
			else
			{
				base.Loyalty += base.UltimateCost;
                base.UltimateUsed = true;
				yield return base.BuffAction<WindGirl>(1, 0, 0, 0, 0.2f);
                yield return new ApplyStatusEffectAction<AccuracyModule>(base.Battle.Player, 1, 0, 0, 0, 0.2f);
                yield return base.SkillAnime;
			}
			yield break;
		}
    }
}


