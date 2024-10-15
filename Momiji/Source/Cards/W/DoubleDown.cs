using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.Intentions;
using LBoL.EntityLib.StatusEffects.Basic;
using LBoLEntitySideloader.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using LBoL.Core.Units;
using System.Linq;
using Momiji.Source.GunName;

namespace Momiji.Source.Cards
{
    public sealed class DoubleDownDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();

            config.GunName = GunNameID.GetGunFromId(400);

            config.Colors = new List<ManaColor>() { ManaColor.White };
            config.Cost = new ManaGroup() { Any = 1, White = 1 };
            config.UpgradedCost = new ManaGroup() { Any = 1, White = 1 };
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Attack;
            config.TargetType = TargetType.SingleEnemy;

            config.Damage = 10;
            config.UpgradedDamage = 10;

            config.Value1 = 1;
            config.UpgradedValue1 = 2;

            //The Accuracy keyword is enough to make an attack accurate.

            config.Illustrator = "ちゃんせ";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(DoubleDownDef))]
    public sealed class DoubleDown : SampleCharacterCard
    {
        //By default, if config.Damage / config.Block / config.Shield are set:
        //The card will deal damage or gain Block/Barrier without having to set anything.
        //Here, this is is equivalent to the following code.


        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            bool notAttacking = false;
            EnemyUnit selectedEnemy = selector.SelectedEnemy;
            notAttacking = selectedEnemy.Intentions.Any(delegate (Intention i)
            {
                if (i is AttackIntention)
                {
                    return false;
                }
                SpellCardIntention spellCardIntention = i as SpellCardIntention;
                if (spellCardIntention != null && spellCardIntention.Damage != null)
                {
                    return false;
                }
                return true;
            });

            yield return base.AttackAction(selector, base.GunName);
            if (notAttacking)
            {
                if (this.IsUpgraded)
                {
                    yield return base.AttackAction(selector, base.GunName);
                    yield return base.AttackAction(selector, base.GunName);
                    yield break;
                }
                yield return base.AttackAction(selector, base.GunName);
                yield break;
            }
            yield break;
        }
    }
}