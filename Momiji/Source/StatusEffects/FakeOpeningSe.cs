using System.Collections.Generic;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoLEntitySideloader.Attributes;
using LBoL.Core.Battle.BattleActions;
using Momiji.Source.StatusEffects;
using Momiji.Source;

namespace Momiji.Source.StatusEffects
{
    public sealed class FakeOpeningSeDef : SampleCharacterStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            config.HasLevel = true;
            config.LevelStackType = LBoL.Base.StackType.Add;
            config.RelativeEffects = new List<string>() { nameof(RetaliationSe) };
            config.Order = 5;
            return config;
        }
    }

    [EntityLogic(typeof(FakeOpeningSeDef))]
    public sealed class FakeOpeningSe : StatusEffect
    {

		protected override void OnAdded(Unit unit)
		{
			base.ReactOwnerEvent<UnitEventArgs>(base.Owner.TurnStarted, new EventSequencedReactor<UnitEventArgs>(this.OnOwnerTurnStarted));
		}

		private IEnumerable<BattleAction> OnOwnerTurnStarted(UnitEventArgs args)
		{
            //At the start of the Player's turn, gain Spirit.
			yield return new ApplyStatusEffectAction<RetaliationSe>(Battle.Player, base.Level, 0, 0, 0, 0.2f);
            //This is equivalent to:
            //yield return new ApplyStatusEffectAction<SampleCharacterTurnGainSpiritSe>(base.Owner, base.Level, 0, 0, 0, 0.2f);
			yield break;
		}
    }
}