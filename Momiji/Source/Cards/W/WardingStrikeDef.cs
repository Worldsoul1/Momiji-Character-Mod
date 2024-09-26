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
    public sealed class WardingStrikeDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();

            config.GunName = GunNameID.GetGunFromId(400);

            config.Colors = new List<ManaColor>() { ManaColor.White };
            config.Cost = new ManaGroup() { White = 2 };
            config.UpgradedCost = new ManaGroup() { White = 1 };
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Attack;
            config.TargetType = TargetType.SingleEnemy;

            config.Damage = 10;
            config.UpgradedDamage = 10;

            config.RelativeEffects = new List<string>() { nameof(Reflect) };
            //The Accuracy keyword is enough to make an attack accurate.

            config.Illustrator = "";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(WardingStrikeDef))]
    public sealed class WardingStrike : SampleCharacterCard
    {
        //attempt at coding for additional damage based on reflection
        public int ReflectDamage
        {
            get
            {
                if (base.Battle != null && base.Battle.Player.HasStatusEffect<Reflect>())
                {
                    return base.Battle.Player.GetStatusEffect<Reflect>().Level;
                }
                return 0;
            }
        }

        // Token: 0x17000178 RID: 376
        // (get) Token: 0x06000CED RID: 3309 RVA: 0x0001826C File Offset: 0x0001646C
        public override int AdditionalDamage
        {
            get
            {
                return this.ReflectDamage;
            }
        }


        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            yield return base.AttackAction(selector, base.GunName);
            yield break;
        }
    }
}