using System.Collections.Generic;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoLEntitySideloader.Attributes;
using LBoL.Core.Battle.BattleActions;
using Momiji.Source.StatusEffects;
using Momiji.Source;

namespace Momiji.Source.StatusEffects
{
    public sealed class SenseWeaknessSeDef : SampleCharacterStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            config.HasLevel = true;
            config.LevelStackType = LBoL.Base.StackType.Add;
            config.RelativeEffects = new List<string>() { nameof(Vulnerable) };
            config.Order = 5;
            return config;
        }
    }
    [EntityLogic(typeof(SenseWeaknessSeDef))]

    public sealed class SenseWeaknessSe : StatusEffect
    {

        protected override void OnAdded(Unit unit)
        {
            base.ReactOwnerEvent<GameEventArgs>(base.Battle.BattleEnding, this.OnBattleEnding);
            base.GameRun.EnemyVulnerableExtraPercentage += base.Level;
        }

        public override bool Stack(StatusEffect other)
        {
            base.GameRun.EnemyVulnerableExtraPercentage += other.Level;
            return base.Stack(other);
        }

        // Token: 0x060003E8 RID: 1000 RVA: 0x0000A914 File Offset: 0x00008B14
        private IEnumerable<BattleAction> OnBattleEnding(GameEventArgs args)
        {
            base.GameRun.EnemyVulnerableExtraPercentage -= base.Level;
            yield break;
        }
    }
}
