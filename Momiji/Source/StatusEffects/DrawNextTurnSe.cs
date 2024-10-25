using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoLEntitySideloader.Attributes;
using Momiji.Source.StatusEffects;

namespace Momiji.Source.StatusEffects
{
    public sealed class DrawnextTurnSeDef : SampleCharacterStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            config.HasLevel = true;
            config.LevelStackType = LBoL.Base.StackType.Add;
            return config;
        }
    }
    [EntityLogic(typeof(DrawnextTurnSeDef))]
    public sealed class DrawNextTurnSe : StatusEffect
    {
        // Token: 0x060002D7 RID: 727 RVA: 0x00007A24 File Offset: 0x00005C24
        protected override void OnAdded(Unit unit)
        {
            base.ReactOwnerEvent<UnitEventArgs>(base.Battle.Player.TurnStarted, new EventSequencedReactor<UnitEventArgs>(this.OnTurnStarted));
        }

        // Token: 0x060002D8 RID: 728 RVA: 0x00007A48 File Offset: 0x00005C48
        private IEnumerable<BattleAction> OnTurnStarted(UnitEventArgs args)
        {
            base.NotifyActivating();
            yield return new DrawManyCardAction(base.Level);
            yield return new RemoveStatusEffectAction(this, true, 0.1f);
            yield break;
        }
    }
}
