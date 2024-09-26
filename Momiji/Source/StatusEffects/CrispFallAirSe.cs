using System;
using System.Collections.Generic;
using System.Linq;
using LBoL.ConfigData;
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
    public sealed class CrispFallAirSeDef : SampleCharacterStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            config.HasLevel = false;
            config.HasDuration = true;
            config.DurationDecreaseTiming = LBoL.Base.DurationDecreaseTiming.EndTurnForExtra;
            return config;
        }

    }
    [EntityLogic(typeof(CrispFallAirSeDef))]
    public sealed class CrispFallAirSe : StatusEffect
    {
        // Token: 0x0600013A RID: 314 RVA: 0x000045BF File Offset: 0x000027BF
        protected override void OnAdded(Unit unit)
        {
            base.ReactOwnerEvent<UnitEventArgs>(base.Owner.TurnStarted, new EventSequencedReactor<UnitEventArgs>(this.OnOwnerTurnStarted));
        }

        // Token: 0x0600013B RID: 315 RVA: 0x000045DE File Offset: 0x000027DE
        private IEnumerable<BattleAction> OnOwnerTurnStarted(UnitEventArgs args)
        {
            if (base.Battle.BattleShouldEnd)
            {
                yield break;
            }
            else
            {
                base.NotifyActivating();
                yield return new AddCardsToHandAction(Library.CreateCards<AirCutter>(base.Level, false), AddCardsType.Normal);
                yield return new RemoveStatusEffectAction(this, true, 0.1f);
            }
            yield break;
        }
    }
}
