using LBoL.Core.Cards;
using LBoL.Core.Intentions;
using LBoL.Core.Units;

namespace Momiji.Source
{
    public class SampleCharacterCard : Card
    {
        // list of intentions: AddCardIntention AttackIntention ChargeIntention ClearIntention CountDownIntention DefendIntention DoNothingIntention EscapeIntention ExplodeAllyIntention ExplodeIntention GrazeIntention HealIntention HexIntention KokoroDarkIntention NegativeEffectIntention PositiveEffectIntention RepairIntention SleepIntention SpawnIntention SpellCardIntention StunIntention UnknownIntention
        // 1 = attack
        // 2 = defend
        // 4 = special
        // e.g. 7 = attack and defend and special
        public int IntentionCheck(EnemyUnit enemyUnit)
        {
            int value = 0;
            bool attack = false;
            bool defend = false;
            bool special = false;
            foreach (Intention intention in enemyUnit.Intentions)
            {
                if ((intention is AddCardIntention || intention is AttackIntention || intention is ExplodeAllyIntention || intention is ExplodeIntention || intention is NegativeEffectIntention)&& attack == false)
                {
                    attack = true;
                    value += 1;
                }
                else if ((intention is ClearIntention || intention is DefendIntention || intention is EscapeIntention || intention is GrazeIntention || intention is HealIntention || intention is RepairIntention || intention is PositiveEffectIntention) && defend == false)
                {
                    defend = true;
                    value += 2;
                }
                else if ((intention is ChargeIntention || intention is DoNothingIntention || intention is HexIntention || intention is SleepIntention || intention is KokoroDarkIntention || intention is SpawnIntention || intention is StunIntention || intention is UnknownIntention) && special == false)
                {
                    special = true;
                    value += 4;
                }
                else if (intention is SpellCardIntention) 
                {
                    SpellCardIntention spellCardIntention = intention as SpellCardIntention;
                    if (spellCardIntention == null || spellCardIntention.Damage == null) 
                    {
                        attack = true;
                        value += 1;
                    }
                    special = true;
                    value += 4;
                }
            }
            return value;
        }
    }
}