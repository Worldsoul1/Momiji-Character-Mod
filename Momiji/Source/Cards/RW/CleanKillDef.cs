using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using Momiji.Source;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;

namespace Momiji.Source.Cards
{
    public sealed class CleanKillDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();

            config.Colors = new List<ManaColor>() { ManaColor.White, ManaColor.Red };
            //Hybrid colors:
            //0 = W/U
            //1 = W/B
            //2 = W/R
            //3 = W/G
            //4 = U/B
            //5 = U/R
            //6 = U/G
            //7 = B/R
            //8 = B/G
            //9 = R/G
            //As of 1.5.1: Colorless hybrid are not supported.    
            config.Cost = new ManaGroup() { Any = 2, White = 1, Red = 1};
            config.UpgradedCost = new ManaGroup() { Any = 2, White = 1, Red = 1 };
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Attack;
            config.TargetType = TargetType.SingleEnemy;

            config.Damage = 24;
            config.UpgradedDamage = 30;

            config.Value1 = 20;
            config.Value2 = 20;

            config.Keywords = Keyword.Exile;
            config.UpgradedKeywords = Keyword.Exile | Keyword.Accuracy;

            config.RelativeEffects = new List<string>() { nameof(Vulnerable) };
            config.UpgradedRelativeEffects = new List<string>() { nameof(Vulnerable) };

            config.Illustrator = "";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(CleanKillDef))]
    public sealed class CleanKill : SampleCharacterCard
    {
         protected override void OnEnterBattle(BattleController battle)
        {
            base.ReactBattleEvent<DieEventArgs>(base.Battle.EnemyDied, new EventSequencedReactor<DieEventArgs>(this.OnEnemyDied));
        }

        // Token: 0x060009C6 RID: 2502 RVA: 0x00014544 File Offset: 0x00012744
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            yield return base.AttackAction(selector, base.GunName);
        }
        private IEnumerable<BattleAction> OnEnemyDied(DieEventArgs args)
        {
            if (args.DieSource == this && !args.Unit.HasStatusEffect<Servant>())
            {
                yield return new GainPowerAction(base.Value1);
            }
            yield break;
        }
    }
}
