using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using Momiji.Source;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.Cards;
using LBoL.Core.Battle.BattleActions;
using LBoL.EntityLib.Cards.Neutral.NoColor;
using LBoL.Core.StatusEffects;

namespace Momiji.Source.Cards
{
    public sealed class AutumnViewDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();
            config.Colors = new List<ManaColor>() { ManaColor.White, ManaColor.Red };
            config.Cost = new ManaGroup() { };
            config.UpgradedCost = new ManaGroup() { };
            config.Rarity = Rarity.Rare;

            config.Type = CardType.Skill;
            config.TargetType = TargetType.Nobody;

            config.Value1 = 12;
            config.UpgradedValue1 = 12;

            config.UpgradedValue2 = 1;

            //The Forbidden Keyword makes the card unplayable. 
            //The Shield keyword is here to add the Barrier description to the tooltip. 
            config.Keywords = Keyword.Replenish | Keyword.AutoExile | Keyword.Forbidden | Keyword.Block;
            config.UpgradedKeywords = Keyword.Replenish | Keyword.AutoExile | Keyword.Forbidden | Keyword.Shield;
            
            config.Illustrator = "";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(AutumnViewDef))]
    public sealed class AutumnView : SampleCharacterCard
    {
        //While in combat, react to the Player's turn ending.
        public override IEnumerable<BattleAction> OnDraw()
        {
            return this.EnterHandReactor();
        }

        public override IEnumerable<BattleAction> OnMove(CardZone srcZone, CardZone dstZone)
        {
            if (dstZone != CardZone.Hand)
            {
                return null;
            }
            return this.EnterHandReactor();
        }
        protected override void OnEnterBattle(BattleController battle)
        {
            if (base.Zone == CardZone.Hand)
            {
                base.React(this.EnterHandReactor());
            }
        }
        private IEnumerable<BattleAction> EnterHandReactor()
            {
                if (base.Battle.BattleShouldEnd)
                {
                    yield break;
                }
                yield return new CastBlockShieldAction(base.Battle.Player, 0, base.Value1, BlockShieldType.Normal, true);
                if (this.IsUpgraded)
                {
                foreach (BattleAction battleAction in base.DebuffAction<FirepowerNegative>(base.Battle.AllAliveEnemies, base.Value2, 0, 0, 0, true, 0.2f))
                {
                    yield return battleAction;
                }
            }
                yield return new ExileCardAction(this);
                yield break;
            }
        }
    }



