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
using Momiji.Source.StatusEffects;

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

            config.RelativeEffects = new List<string>() { nameof(DefensiveIntention) };
            config.UpgradedRelativeEffects = new List<string>() { nameof(DefensiveIntention) };
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
            int intention = 0;
            EnemyUnit selectedEnemy = selector.SelectedEnemy;
            intention = base.IntentionCheck(selectedEnemy);
            if (intention == 2 || intention == 3 || intention == 6 || intention == 7)
            {
                notAttacking = true;
            }

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