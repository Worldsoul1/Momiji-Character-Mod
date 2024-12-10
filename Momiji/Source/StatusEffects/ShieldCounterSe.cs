using System.Collections.Generic;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoLEntitySideloader.Attributes;
using Momiji.Source.StatusEffects;
using Momiji.Source;
using LBoL.Base;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.EntityLib.StatusEffects.Basic;
using Momiji.Source.GunName;
namespace Momiji.Source.StatusEffects
{
    public sealed class ShieldCounterSeDef : SampleCharacterStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            config.HasLevel = true;
            return config;
        }
    }
    [EntityLogic(typeof(ShieldCounterSeDef))]
    public sealed class ShieldCounterSe : StatusEffect
    {
        protected override void OnAdded(Unit unit)
        {
            base.ReactOwnerEvent<DamageEventArgs>(base.Battle.Player.DamageDealt, this.OnPlayerDamageDealt);
        }

        public IEnumerable<BattleAction> OnPlayerDamageDealt(DamageEventArgs args)
        {
            if ((args.ActionSource is TempElectric || args.ActionSource is Reflect) && args.Target != null)
            {
                base.NotifyActivating();
                    if (args.Target.IsAlive)
                    {
                        yield return new ApplyStatusEffectAction<Vulnerable>(args.Target, 0, base.Level, 0, 0, 0.2f);
                    //DamageInfo must be either Reaction or HpLoss since Attack could potentially trigger an infinite loop without additional checks.
                    }
                yield break;
            }
        }

    }
}