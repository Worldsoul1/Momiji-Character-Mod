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
    public sealed class DestabilizeSeDef : SampleCharacterStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            config.HasLevel = true;
            config.LevelStackType = LBoL.Base.StackType.Add;
            return config;
        }
    }
    [EntityLogic(typeof(DestabilizeSeDef))]
    public sealed class DestabilizeSe : StatusEffect
    {
        protected override void OnAdded(Unit unit)
        {
            base.ReactOwnerEvent<DamageEventArgs>(base.Battle.Player.DamageDealt, new EventSequencedReactor<DamageEventArgs>(this.OnPlayerDamageDealt));
        }

        // Token: 0x06000045 RID: 69 RVA: 0x0000273A File Offset: 0x0000093A
        private IEnumerable<BattleAction> OnPlayerDamageDealt(DamageEventArgs args)
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
                    yield return new CastBlockShieldAction(base.Battle.Player, base.Level, 0, BlockShieldType.Direct, false);
                }
            }
            yield break;
        }
    }
}
