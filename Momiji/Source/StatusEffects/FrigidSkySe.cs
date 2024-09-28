using LBoL.ConfigData;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Battle;
using LBoL.Core.StatusEffects;
using LBoL.Core;
using LBoL.Core.Units;
using LBoL.Core.Cards;
using LBoLEntitySideloader.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using LBoL.Base;
using LBoL.EntityLib.StatusEffects.Cirno;
using LBoL.Core.Randoms;
using LBoL.EntityLib.Cards.Character.Sakuya;

namespace Momiji.Source.StatusEffects
{
    public sealed class FrigidSkySeDef : SampleCharacterStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            config.HasLevel = false;
            return config;
        }
    }
    [EntityLogic(typeof(FrigidSkySeDef))]
    public sealed class FrigidSkySe : StatusEffect
    {
        protected override void OnAdded(Unit unit)
        {
            base.ReactOwnerEvent<DamageEventArgs>(base.Battle.Player.DamageDealt, this.OnPlayerDamageDealt);
        }
        private IEnumerable<BattleAction> OnPlayerDamageDealt(DamageEventArgs args)
        {
            if (base.Battle.BattleShouldEnd)
            {
                yield break;
            }
            if (args.Cause == ActionCause.Card)
            {
                Card card = args.ActionSource as Card;
                DamageInfo damageInfo = args.DamageInfo;

                if (card != null && card.BaseName == nameof(AirCutter) && damageInfo.Damage > 0f)
                {
                    yield return base.DebuffAction<Cold>(args.Target, 0, 0, 0, 0, true, 0.03f);
                }
            }
            yield break;
        }


        // Token: 0x06000045 RID: 69 RVA: 0x0000273A File Offset: 0x0000093A
    }
}
