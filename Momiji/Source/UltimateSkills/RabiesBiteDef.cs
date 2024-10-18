using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Battle;
using LBoL.Core.Cards;
using LBoL.Core.Units;
using LBoL.Core;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using Momiji.Source.GunName;
using Momiji.Source.Ultimate;
using LBoL.Core.StatusEffects;

namespace Momiji.Source.Ultimate
{
    public sealed class RabiesBiteDef : SampleCharacterUltTemplate
    {
        public override UltimateSkillConfig MakeConfig()
        {
            UltimateSkillConfig config = GetDefaulUltConfig();
            config.Damage = 15;
            config.Value1 = 3;
            config.Value2 = 4;
            config.Keywords = Keyword.Accuracy;

            // Add the relative status effects in the description box.   
            config.RelativeEffects = new List<string>() { nameof(Weak) };
            return config;
        }
    }

    [EntityLogic(typeof(RabiesBiteDef))]
    public sealed class RabiesBite : UltimateSkill
    {
        public RabiesBite()
        {
            base.TargetType = TargetType.SingleEnemy;
            base.GunName = GunNameID.GetGunFromId(4158);
        }

        protected override IEnumerable<BattleAction> Actions(UnitSelector selector)
        {
            EnemyUnit enemy = selector.GetEnemy(base.Battle);
            yield return PerformAction.Spell(base.Battle.Player, nameof(RabiesBite));
            yield return new DamageAction(base.Owner, enemy, this.Damage, base.GunName, GunType.Single);
            yield return new DamageAction(base.Owner, enemy, this.Damage, base.GunName, GunType.Single);

            //Only apply the status effect if the enemy is still alive after the attack. 
            if (enemy.IsAlive)
            {
                yield return new ApplyStatusEffectAction<Vulnerable>(enemy, 0, base.Value1, 0, 0, 0.2f);
                yield break;
            }
            
        }
        public void OnEnterBattle(BattleController battle)
        {
            base.Owner.ReactBattleEvent<DieEventArgs>(base.Battle.EnemyDied, this.OnEnemyDied);
        }

        // Token: 0x060009C6 RID: 2502 RVA: 0x00014544 File Offset: 0x00012744
        private IEnumerable<BattleAction> OnEnemyDied(DieEventArgs args)
        {
            if (args.DieSource == this && !args.Unit.HasStatusEffect<Servant>())
            {
                base.GameRun.GainMaxHp(base.Value2, true, true);
                yield return PerformAction.Sfx("Shengyan", 0f);
            }
            yield break;
        }
    }
}