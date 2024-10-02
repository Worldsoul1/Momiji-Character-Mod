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
    public sealed class WoodlandSentrySeDef : SampleCharacterStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            config.HasLevel = true;
            config.LevelStackType = LBoL.Base.StackType.Add;
            config.Order = 10;
            return config;
        }
    }

    [EntityLogic(typeof(WoodlandSentrySeDef))]
    public sealed class WoodlandSentrySe : StatusEffect
    {
        // Token: 0x0600013A RID: 314 RVA: 0x000045BF File Offset: 0x000027BF
        protected override void OnAdded(Unit unit)
        {
            base.ReactOwnerEvent<UnitEventArgs>(base.Battle.Player.TurnEnding, new EventSequencedReactor<UnitEventArgs>(this.OnTurnEnding));
        }

        // Token: 0x0600013B RID: 315 RVA: 0x000045DE File Offset: 0x000027DE
        private IEnumerable<BattleAction> OnTurnEnding(UnitEventArgs args)
        {
            int num = base.Battle.HandZone.Count * base.Level;
            if (num > 0)
            {
                base.NotifyActivating();
                yield return new CastBlockShieldAction(base.Battle.Player, num, 0, BlockShieldType.Normal, true);
            }
            yield break;
        }
    }
}