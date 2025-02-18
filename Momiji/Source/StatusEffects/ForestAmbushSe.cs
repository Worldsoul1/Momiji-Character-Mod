﻿using System.Collections.Generic;
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
    public sealed class ForestAmbushSeDef : SampleCharacterStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            config.HasLevel = false;
            return config;
        }
    }
    [EntityLogic(typeof(ForestAmbushSeDef))]
    public sealed class ForestAmbushSe : StatusEffect
    {
        protected override void OnAdded(Unit unit)
        {
            base.ReactOwnerEvent<DamageEventArgs>(base.Battle.Player.DamageDealt, this.OnPlayerDamageDealt);
            base.ReactOwnerEvent<UnitEventArgs>(base.Battle.Player.TurnEnded, this.OnTurnEnding);
        }

        public IEnumerable<BattleAction> OnPlayerDamageDealt(DamageEventArgs args)
        {
            if (args.Source == base.Battle.Player && args.Target != null && args.DamageInfo.DamageType == DamageType.Attack)
            {
                base.NotifyActivating();
                if (args.Target.IsAlive)
                {
                    yield return new DamageAction(base.Owner, args.Target, DamageInfo.Reaction((float)base.Battle.Player.GetStatusEffect<RetaliationSe>().Level, false), GunNameID.GetGunFromId(400), GunType.Single);
                    //DamageInfo must be either Reaction or HpLoss since Attack could potentially trigger an infinite loop without additional checks.
                }                
            }
        }
        private IEnumerable<BattleAction> OnTurnEnding(UnitEventArgs args)
        {
            yield return new RemoveStatusEffectAction(this, true, 0.1f);
            yield break;
        }
    }
}
