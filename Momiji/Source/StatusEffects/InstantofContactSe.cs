using System.Collections.Generic;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoLEntitySideloader.Attributes;
using LBoL.EntityLib.StatusEffects.ExtraTurn;
using Momiji.Source.StatusEffects;
using Momiji.Source;
using LBoL.Base;
using LBoL.Core.Cards;
namespace Momiji.Source.StatusEffects
{
    public sealed class InstantofContactSeDef : SampleCharacterStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            return config;
        }
           
    }
    [EntityLogic(typeof(InstantofContactSeDef))]
    public sealed class InstantofContactSe : ExtraTurnPartner
    {
        protected override void OnAdded(Unit unit) 
        {
            base.ReactOwnerEvent<DamageEventArgs>(base.Owner.DamageReceived, new EventSequencedReactor<DamageEventArgs>(this.OnPlayerDamageDealt));
            base.ThisTurnActivating = false;
            base.HandleOwnerEvent<UnitEventArgs>(base.Battle.Player.TurnStarting, (UnitEventArgs args) =>
            {
                if (base.Battle.Player.IsExtraTurn && !base.Battle.Player.IsSuperExtraTurn && base.Battle.Player.GetStatusEffectExtend<ExtraTurnPartner>() == this)
                {
                    base.ThisTurnActivating = true;
                }
            });
            base.ReactOwnerEvent<UnitEventArgs>(base.Battle.Player.TurnEnded, this.OnPlayerTurnEnded);
        }

        private IEnumerable<BattleAction> OnPlayerTurnEnded(UnitEventArgs args)
        {
            if (base.ThisTurnActivating)
            {
                yield return new RemoveStatusEffectAction(this, true, 0.1f);
            }
            yield break;
        }
        // Token: 0x06000045 RID: 69 RVA: 0x0000273A File Offset: 0x0000093A
        private IEnumerable<BattleAction> OnPlayerDamageDealt(DamageEventArgs args)
        {
            if (base.Battle.BattleShouldEnd)
            {
                yield break;
            }
            base.NotifyActivating();
            yield return new ApplyStatusEffectAction<RetaliationSe>(Battle.Player, base.Level, 0, 0, 0, 0.2f);
            yield break;
        }
    }
}