using System.Collections.Generic;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoLEntitySideloader.Attributes;
using Momiji.Source.StatusEffects;
using Momiji.Source;
using LBoL.Base;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.EntityLib.StatusEffects.Basic;
using Momiji.Source.GunName;
namespace Momiji.Source.StatusEffects
{
    public sealed class DistractSeDef : SampleCharacterStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            config.HasLevel = false;
            return config;
        }
    }
    [EntityLogic(typeof(DistractSeDef))]
    public sealed class DistractSe : StatusEffect
    {
        protected override void OnAdded(Unit unit)
        {
            base.ReactOwnerEvent<DamageDealingEventArgs>(base.Battle.Player.DamageDealing, this.OnPlayerDamageDealt);
        }

        public IEnumerable<BattleAction> OnPlayerDamageDealt(DamageDealingEventArgs args)
        {
            if (args.Source == base.Battle.Player && args.Targets != null && args.DamageInfo.DamageType == DamageType.Attack)
            {
                foreach (EnemyUnit enemyUnit in args.Targets)
                {
                    if (enemyUnit.IsAlive && enemyUnit.HasStatusEffect<Vulnerable>())
                    {
                        yield return new ApplyStatusEffectAction<Weak>(enemyUnit, 0, 1, 0, 0, 0.2f);
                        //DamageInfo must be either Reaction or HpLoss since Attack could potentially trigger an infinite loop without additional checks.
                    }
                }
            }
        }
    }
}