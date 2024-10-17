using System.Collections.Generic;
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
            config.HasLevel = false;
            config.RelativeEffects = new List<string>() { nameof(Vulnerable) };
            config.Order = 10;
            return config;
        }
    }

    [EntityLogic(typeof(HowlingMountainWindSeDef))]
    public sealed class HowlingMountainWindSe : StatusEffect
    {

    }
}