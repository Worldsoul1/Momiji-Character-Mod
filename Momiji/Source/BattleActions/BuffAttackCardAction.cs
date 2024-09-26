using System.Collections.Generic;
using System.Linq;
using LBoL.Base;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;

namespace Momiji.Source.BattleActions
{
    public sealed class BuffAttackCardAction : EventBattleAction<BuffAttackEventArgs>
    {             
        internal BuffAttackCardAction(Card card = null, int amount = 0)
		{
			base.Args = new BuffAttackEventArgs
			{ 
                Card = card,
                Amount = amount,
			};
		}

        public override IEnumerable<Phase> GetPhases()
        {
            yield return base.CreatePhase("Main", delegate
			{
                if(Args.Card != null && Args.Card.Config.Type == CardType.Attack)
                {
                    Args.Card.DeltaDamage += Args.Amount;
                }
            }, hasViewer: true);
            yield break;
        }
    }
}