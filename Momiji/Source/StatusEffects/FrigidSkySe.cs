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


        // Token: 0x06000045 RID: 69 RVA: 0x0000273A File Offset: 0x0000093A
    }
}
