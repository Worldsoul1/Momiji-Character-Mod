using System.Collections.Generic;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoL.EntityLib.StatusEffects.Basic;
using LBoLEntitySideloader.Attributes;
using LBoLEntitySideloader.Resource;
using Momiji.Source.StatusEffects;
namespace Momiji.Source.StatusEffects
{
    public sealed class RetaliationSeDef : SampleCharacterStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            config.RelativeEffects = new List<string>() { nameof(Reflect) };
            config.HasLevel = true;
            config.LevelStackType = LBoL.Base.StackType.Add;
            return config;
        }
    }

    [EntityLogic(typeof(RetaliationSeDef))]
    public sealed class RetaliationSe : StatusEffect
    {

        protected override void OnAdded(Unit unit)
        {
            base.ReactOwnerEvent<UnitEventArgs>(base.Owner.TurnStarted, new EventSequencedReactor<UnitEventArgs>(this.OnOwnerTurnStarted));
        }

        private IEnumerable<BattleAction> OnOwnerTurnStarted(UnitEventArgs args)
        {
            //At the start of the Player's turn, gain Spirit.
            yield return base.BuffAction<Reflect>(base.Level, 0, 0, 0, 0.2f);
            //This is equivalent to:
            //yield return new ApplyStatusEffectAction<SampleCharacterTurnGainSpiritSe>(base.Owner, base.Level, 0, 0, 0, 0.2f);
            yield break;
        }
    }
}
