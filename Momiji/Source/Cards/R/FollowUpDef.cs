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

namespace Momiji.Source.Cards
{
    public sealed class FollowUpDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();

            config.GunName = GunNameID.GetGunFromId(400);

            config.Colors = new List<ManaColor>() { ManaColor.Red };
            config.Cost = new ManaGroup() { Red = 1 };
            config.UpgradedCost = new ManaGroup() { Red = 1 };
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Attack;
            config.TargetType = TargetType.SingleEnemy;

            config.Damage = 9;
            config.UpgradedDamage = 13;

            config.Mana = new ManaGroup() { Red = 1 };
            config.UpgradedMana = new ManaGroup() { Red = 2 };

            config.RelativeEffects = new List<string>() { nameof(Vulnerable) };
            config.UpgradedRelativeEffects = new List<string>() { nameof(Vulnerable) };

            //The Accuracy keyword is enough to make an attack accurate.

            config.Illustrator = "cuon";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(FollowUpDef))]
    public sealed class FollowUp : SampleCharacterCard
    {
        //By default, if config.Damage / config.Block / config.Shield are set:
        //The card will deal damage or gain Block/Barrier without having to set anything.
        //Here, this is is equivalent to the following code.


        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            bool flag = selector.SelectedEnemy.HasStatusEffect<Vulnerable>();
            yield return base.AttackAction(selector, base.GunName);
            if (flag)
            {
                yield return new GainManaAction(Mana);
            }
        }
    }
}

