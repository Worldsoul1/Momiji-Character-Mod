using System.Collections.Generic;
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
    public sealed class TwinFangSeDef : SampleCharacterStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            config.HasLevel = false;
            return config;
        }

    }
    [EntityLogic(typeof(TwinFangSeDef))]
    public sealed class TwinFangSe : StatusEffect
    {
        protected override void OnAdded(Unit unit)
        {
            base.ReactOwnerEvent<StatusEffectEventArgs>(base.Battle.Player.StatusEffectRemoved, this.StatusEffectRemoved);
        
        }


        // Token: 0x060000DF RID: 223 RVA: 0x00003947 File Offset: 0x00001B47
        private IEnumerable<BattleAction> StatusEffectRemoved(StatusEffectEventArgs args)
        {
            if (args.ActionSource is Reflect)
            { 
                int reflectLevel = args.Effect.Level;
                if (reflectLevel > 0f)
                {
                    yield return new ApplyStatusEffectAction<Reflect>(base.Battle.Player, reflectLevel, 0, 0, 0, 0.2f);
                    yield return new RemoveStatusEffectAction(this, true, 0.1f);
                }
            }
            yield break;
        }

    }
}