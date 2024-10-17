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
    public sealed class IndiscriminateRevengeSeDef : SampleCharacterStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            config.HasLevel = false;
            return config;
        }

    }
    [EntityLogic(typeof(IndiscriminateRevengeSeDef))]
    public sealed class IndiscriminateRevengeSe : StatusEffect
    {
        bool hasActivated = false;
        protected override void OnAdded(Unit unit)
        {
            base.ReactOwnerEvent<StatusEffectEventArgs>(base.Battle.Player.StatusEffectRemoved, this.StatusEffectRemoved);
        }


        // Token: 0x060000DF RID: 223 RVA: 0x00003947 File Offset: 0x00001B47
        private IEnumerable<BattleAction> StatusEffectRemoved(StatusEffectEventArgs args)
        {
            if (args.ActionSource is Reflect)
            {
                int retalLevel = base.Battle.Player.GetStatusEffect<RetaliationSe>().Level;
                if (retalLevel > 0f )
                {
                    yield return new DamageAction(base.Battle.Player, base.Battle.EnemyGroup.Alives, DamageInfo.Reaction((float)retalLevel, false));
                }
            }
            yield break;
        }
    }
}