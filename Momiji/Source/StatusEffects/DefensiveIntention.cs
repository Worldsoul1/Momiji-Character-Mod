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
using Momiji.Source.StatusEffects;

namespace Momiji.Source.StatusEffects
{
    public sealed class DefensiveIntentionDef : SampleCharacterStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            config.HasLevel = false;
            config.Order = 5;
            return config;
        }
    }

    [EntityLogic(typeof(DefensiveIntentionDef))]
    public sealed class DefensiveIntention : StatusEffect
    {
    }
}
