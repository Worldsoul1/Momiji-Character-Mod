﻿using System.Collections.Generic;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoLEntitySideloader.Attributes;
using LBoL.Core.Battle.BattleActions;
using Momiji.Source.StatusEffects;
using LBoL.EntityLib.StatusEffects.Basic;
using Momiji.Source;

namespace Momiji.Source.StatusEffects
{
    public sealed class HowlingMountainWindSeDef : SampleCharacterStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            config.HasLevel = true;
            config.LevelStackType = LBoL.Base.StackType.Add;
            config.RelativeEffects = new List<string>() { nameof(Reflect) };
            config.Order = 10;
            return config;
        }
    }

    [EntityLogic(typeof(HowlingMountainWindSeDef))]
    public sealed class HowlingMountainWindSe : StatusEffect
    {

        protected override void OnAdded(Unit unit)
        {
            base.ReactOwnerEvent<CardUsingEventArgs>(base.Battle.CardUsed, new EventSequencedReactor<CardUsingEventArgs>(this.OnCardUsed));
        }

        // Token: 0x06000092 RID: 146 RVA: 0x000030DC File Offset: 0x000012DC
        private IEnumerable<BattleAction> OnCardUsed(CardUsingEventArgs args)
        {
            if (base.Battle.BattleShouldEnd)
            {
                yield break;
            }
            if (args.Card is AirCutter)
            {
                base.NotifyActivating();
                yield return new ApplyStatusEffectAction<Reflect>(base.Owner, new int?(base.Level), null, null, null, 0f, true);
            }
            yield break;
        }
    }
}