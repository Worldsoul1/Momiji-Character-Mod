using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core.Battle;
using LBoL.Core.Cards;
using LBoL.Core;
using LBoLEntitySideloader.Attributes;
using Momiji.Source.GunName;
using System;
using System.Collections.Generic;
using System.Text;
using LBoL.EntityLib.StatusEffects.Basic;
using LBoL.Core.StatusEffects;

namespace Momiji.Source.Cards
{
        public sealed class BloodScentDef : SampleCharacterCardTemplate
        {
            public override CardConfig MakeConfig()
            {
                CardConfig config = GetCardDefaultConfig();

                config.GunName = GunNameID.GetGunFromId(400);

                config.Colors = new List<ManaColor>() { ManaColor.Red };
                config.Cost = new ManaGroup() { Any = 1, Red = 1 };
                config.UpgradedCost = new ManaGroup() { Any = 1, Red = 1 };
                config.Rarity = Rarity.Common;

                config.Type = CardType.Attack;
                config.TargetType = TargetType.SingleEnemy;

                config.Damage = 12;
                config.UpgradedDamage = 16;

                config.Value1 = 12;
                config.UpgradedValue1 = 16;

                config.RelativeEffects = new List<string>() { nameof(Reflect), nameof(Vulnerable) };
                config.UpgradedRelativeEffects = new List<string>() { nameof(Reflect), nameof(Vulnerable) };

            //The Accuracy keyword is enough to make an attack accurate.

            config.Illustrator = "";

                config.Index = CardIndexGenerator.GetUniqueIndex(config);
                return config;
            }
        }

        [EntityLogic(typeof(BloodScentDef))]
        public sealed class BloodScent : SampleCharacterCard
        {
            //By default, if config.Damage / config.Block / config.Shield are set:
            //The card will deal damage or gain Block/Barrier without having to set anything.
            //Here, this is is equivalent to the following code.


            protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
            {
            if (base.PendingTarget.HasStatusEffect<Vulnerable>())
            {
                yield return base.BuffAction<Reflect>(base.Value1, 0, 0, 0, 0.2f);
                if (base.Battle.Player.HasStatusEffect<Reflect>())
                {
                    base.Battle.Player.GetStatusEffect<Reflect>().Gun = (this.IsUpgraded ? "金刚体B" : "金刚体");
                }
            }
            yield return base.AttackAction(selector, base.GunName);
            }
        }
    }

