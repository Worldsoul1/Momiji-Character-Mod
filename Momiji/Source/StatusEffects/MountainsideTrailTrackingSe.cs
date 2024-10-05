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
namespace Momiji.Source.StatusEffects
{
    public sealed class MountainsideTrailTrackingSeDef : SampleCharacterStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            config.HasLevel = true;
            config.LevelStackType = LBoL.Base.StackType.Add;
            return config;
        }
    }
    [EntityLogic(typeof(MountainsideTrailTrackingSeDef))]
    public sealed class MountainsideTrailTrackingSe : StatusEffect
    {
        bool hasActivated = false;
        protected override void OnAdded(Unit unit)
        {
            base.ReactOwnerEvent<DamageDealingEventArgs>(base.Battle.Player.DamageDealing, new EventSequencedReactor<DamageDealingEventArgs>(this.OnPlayerDamageDealing));
            base.ReactOwnerEvent<UnitEventArgs>(base.Owner.TurnStarted, new EventSequencedReactor<UnitEventArgs>(this.OnOwnerTurnStarted));
        }
        // Token: 0x06000045 RID: 69 RVA: 0x0000273A File Offset: 0x0000093A
        private IEnumerable<BattleAction> OnPlayerDamageDealing(DamageDealingEventArgs args)
        {
            if (base.Battle.BattleShouldEnd)
            {
                yield break;
            }
            if (args.Cause == ActionCause.Card)
            {
                DamageInfo damageInfo = args.DamageInfo;

                if (damageInfo.Damage > 0f)
                {
                    foreach (Unit enemyUnit in args.Targets)
                    {
                        if (enemyUnit.HasStatusEffect<Vulnerable>())
                        {
                            yield return new DrawManyCardAction(base.Level);
                            hasActivated = true;
                        }
                    }
                }
            }
        }
        private IEnumerable<BattleAction> OnOwnerTurnStarted(UnitEventArgs args)
        {
            if (base.Battle.BattleShouldEnd) { yield break; }
            if (hasActivated)
            {
                hasActivated = false;
            }
        }
    }
}