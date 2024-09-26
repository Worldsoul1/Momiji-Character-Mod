using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.Core;
using LBoL.Core.StatusEffects;
using LBoLEntitySideloader.Attributes;
using Momiji.Source.GunName;
using System;
using System.Collections.Generic;
using System.Text;
using LBoL.EntityLib.StatusEffects.Basic;
using LBoL.Core.Units;

namespace Momiji.Source.Cards
{
    public sealed class OverhandBladeDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();

            config.GunName = GunNameID.GetGunFromId(400);

            config.Colors = new List<ManaColor>() { ManaColor.Red };
            config.Cost = new ManaGroup() { Any = 2, Red = 1 };
            config.UpgradedCost = new ManaGroup() { Any = 1, Red = 1 };
            config.Rarity = Rarity.Common;

            config.Type = CardType.Attack;
            config.TargetType = TargetType.SingleEnemy;

            config.Damage = 15;
            config.UpgradedDamage = 15;

            config.Value1 = 2;
            config.UpgradedValue1 = 2;

            config.RelativeEffects = new List<string>() { nameof(Vulnerable), nameof(Weak) };

            //The Accuracy keyword is enough to make an attack accurate.

            config.Illustrator = "";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(OverhandBladeDef))]
    public sealed class OverhandBlade : SampleCharacterCard
    {
        //By default, if config.Damage / config.Block / config.Shield are set:
        //The card will deal damage or gain Block/Barrier without having to set anything.
        //Here, this is is equivalent to the following code.


        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            EnemyUnit selectedEnemy = selector.SelectedEnemy;
            yield return base.AttackAction(selector, base.GunName);
            if (selectedEnemy.HasStatusEffect<Vulnerable>())
            {
                yield return new ApplyStatusEffectAction<Weak>(selectedEnemy, 0, base.Value1, 0, 0, 0.2f);
            }
            if (selectedEnemy.HasStatusEffect<Weak>())
            {
                yield return new ApplyStatusEffectAction<Vulnerable>(selectedEnemy, 0, base.Value1, 0, 0, 0.2f);
            }
        }
    }
}
