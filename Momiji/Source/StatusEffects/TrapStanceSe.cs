﻿using System.Collections.Generic;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoLEntitySideloader.Attributes;
using LBoL.EntityLib.StatusEffects.Basic;
using Momiji.Source.StatusEffects;
using Momiji.Source;
using LBoL.Base;
using LBoL.Core.Cards;
namespace Momiji.Source.StatusEffects
{
    public sealed class TrapStanceSeDef : SampleCharacterStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            config.HasLevel = false;
            return config;
        }

    }
    [EntityLogic(typeof(TrapStanceSeDef))]
    public sealed class TrapStanceSe : StatusEffect
    {
        bool hasActivated = false;
        protected override void OnAdded(Unit unit)
        {
            base.ReactOwnerEvent<StatusEffectEventArgs>(base.Battle.Player.StatusEffectRemoved, this.StatusEffectRemoved);
            base.ReactOwnerEvent<UnitEventArgs>(base.Owner.TurnStarted, new EventSequencedReactor<UnitEventArgs>(this.OnOwnerTurnStarted));
        }


        // Token: 0x060000DF RID: 223 RVA: 0x00003947 File Offset: 0x00001B47
        private IEnumerable<BattleAction> StatusEffectRemoved(StatusEffectEventArgs args)
        {
            if (args.ActionSource is Reflect)
            {
                int retalLevel = base.Battle.Player.GetStatusEffect<RetaliationSe>().Level;
                if (retalLevel > 0f && hasActivated == false)
                {
                    yield return new ApplyStatusEffectAction<Reflect>(base.Battle.Player, retalLevel, 0, 0, 0, 0.2f);
                    if (base.Battle.Player.HasStatusEffect<Reflect>())
                    {
                        base.Battle.Player.GetStatusEffect<Reflect>().Gun = ("心抄斩");
                    }
                    hasActivated = true;
                }
            }
            yield break;
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