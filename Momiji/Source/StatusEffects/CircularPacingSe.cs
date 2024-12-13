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
    public sealed class CircularPacingSeDef : SampleCharacterStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            config.HasLevel = true;
            config.LevelStackType = LBoL.Base.StackType.Add;
            return config;
        } 
    }
    [EntityLogic(typeof(CircularPacingSeDef))]
    public sealed class CircularPacingSe : StatusEffect
    {

        // Token: 0x06000045 RID: 69 RVA: 0x0000273A File Offset: 0x0000093A
        protected override void OnAdded(Unit unit)
        {
            foreach (EnemyUnit enemyUnit in base.Battle.AllAliveEnemies)
            {
                base.ReactOwnerEvent<StatusEffectApplyEventArgs>(enemyUnit.StatusEffectAdded, new EventSequencedReactor<StatusEffectApplyEventArgs>(this.OnEnemyStatusEffectAdded));
            }
            base.HandleOwnerEvent<UnitEventArgs>(base.Battle.EnemySpawned, new GameEventHandler<UnitEventArgs>(this.OnEnemySpawned));
        }

        // Token: 0x060000B5 RID: 181 RVA: 0x0000354C File Offset: 0x0000174C
        private IEnumerable<BattleAction> OnEnemyStatusEffectAdded(StatusEffectApplyEventArgs args)
        {
            if (args.Effect.Type == StatusEffectType.Negative)
            {
                yield return new CastBlockShieldAction(base.Battle.Player, 0, base.Level, BlockShieldType.Direct, false);
            }
            yield break;
        }

        // Token: 0x060000B6 RID: 182 RVA: 0x00003563 File Offset: 0x00001763
        private void OnEnemySpawned(UnitEventArgs args)
        {
            base.ReactOwnerEvent<StatusEffectApplyEventArgs>(args.Unit.StatusEffectAdded, new EventSequencedReactor<StatusEffectApplyEventArgs>(this.OnEnemyStatusEffectAdded));
        }
    }
}
