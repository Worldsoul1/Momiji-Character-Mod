using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoLEntitySideloader;
using LBoLEntitySideloader.Attributes;
using LBoLEntitySideloader.Entities;
using LBoLEntitySideloader.Resource;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Momiji.Source;
using Momiji.Source.StatusEffects;

namespace Momiji.Source.StatusEffects
{
    public sealed class SmellofDeathSeDef : SampleCharacterStatusEffectTemplate
    {

        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            config.HasLevel = true;
            return config;
        }
    }

    [EntityLogic(typeof(SmellofDeathSeDef))]
    public sealed class SmellofDeathSe : StatusEffect
    {
        public int FinalDamage { get => this.Owner.MaxHp * this.Level / 2; }

        protected override void OnAdded(Unit unit)
        {
            if (!(unit is EnemyUnit))
            {
                Debug.LogError("Cannot add DeathExplode to " + unit.GetType().Name);
            }
            else
            {
                this.ReactOwnerEvent(this.Owner.Dying, OnDying);
            }
        }

        private IEnumerable<BattleAction> OnDying(DieEventArgs args)
        {
            DieCause dieCause = args.DieCause;
            if (dieCause == DieCause.Explode || dieCause == DieCause.ServantDie)
            {
                yield break;
            }

            base.NotifyActivating();
            args.CancelBy(this);

            Unit owner = this.Owner;

            yield return new DamageAction(owner as EnemyUnit, this.Battle.AllAliveEnemies.Where(e => e != owner), DamageInfo.Attack(FinalDamage), "冰封噩梦B");
            yield return new DieAction(owner, DieCause.Explode, owner, this);
        }
    }
}