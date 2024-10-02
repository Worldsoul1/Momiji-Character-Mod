using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Battle;
using LBoL.Core.Cards;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoL.Core;
using LBoL.EntityLib.EnemyUnits.Normal.Guihuos;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using LBoL.ConfigData;
using Momiji.Source.GunName;
using LBoLEntitySideloader.Attributes;
using Momiji.Source.StatusEffects;

namespace Momiji.Source.StatusEffects
{
    internal class SmellofDeathSeDef : SampleCharacterStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            config.HasLevel = false;
            return config;
        }
    }
}
[EntityLogic(typeof(SmellofDeathSeDef))]
public abstract class SmellofDeathSe : StatusEffect
{
    // Token: 0x060001E7 RID: 487 RVA: 0x00005DE8 File Offset: 0x00003FE8
    protected override void OnAdded(Unit unit)
    {
        if (!(unit is EnemyUnit))
        {
            Debug.LogError("Cannot add DeathExplode to " + unit.GetType().Name);
        }
        base.ReactOwnerEvent<DieEventArgs>(base.Owner.Dying, new EventSequencedReactor<DieEventArgs>(this.OnDying));
    }

    // Token: 0x060001E8 RID: 488 RVA: 0x00005E34 File Offset: 0x00004034
    private IEnumerable<BattleAction> OnDying(DieEventArgs args)
    {
        DieCause dieCause = args.DieCause;
        if (dieCause == DieCause.Explode || dieCause == DieCause.ServantDie)
        {
            yield break;
        }
        base.NotifyActivating();
        args.CancelBy(this);
        Unit owner = base.Owner;
        int num = base.Owner.MaxHp / 2;

        foreach (EnemyUnit enemyUnit in base.Battle.AllAliveEnemies)
        {
            yield return new DamageAction((EnemyUnit)base.Owner, enemyUnit, DamageInfo.Attack((float)num, false), GunNameID.GetGunFromId(400), GunType.Single);
        }
       yield break;
    }

    // Token: 0x0400000E RID: 14

}
