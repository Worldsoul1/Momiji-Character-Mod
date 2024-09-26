using System;
using System.Collections.Generic;
using System.Linq;
using LBoL.ConfigData;
using LBoL.Base;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoL.EntityLib.Cards.Neutral.NoColor;
using LBoLEntitySideloader.Attributes;
using Momiji.Source.Cards;

namespace Momiji.Source.StatusEffects
{
    public sealed class FeedOnTheWeakSeDef : SampleCharacterStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            config.HasLevel = false;
            config.HasDuration = false;
            return config;
        }

    }
    [EntityLogic(typeof(FeedOnTheWeakSeDef))]
    public sealed class FeedOnTheWeakSe : StatusEffect
    {
        // Token: 0x0600013A RID: 314 RVA: 0x000045BF File Offset: 0x000027BF
        public void OnEnterBattle(BattleController battle)
        {
            base.Owner.ReactBattleEvent<DieEventArgs>(base.Battle.EnemyDied, this.OnEnemyDied);
        }

        // Token: 0x0600013B RID: 315 RVA: 0x000045DE File Offset: 0x000027DE
        private IEnumerable<BattleAction> OnEnemyDied(DieEventArgs args)
        {
            if (args.DieSource == this && !args.Unit.HasStatusEffect<Servant>())
            {
                yield return new GainPowerAction(10);
                yield return new HealAction(base.Battle.Player, base.Battle.Player, 4, HealType.Normal, 0.2f);
            }
            yield break;
        }
    }
}
