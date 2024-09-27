using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using Momiji.Source;
using Momiji.Source.GunName;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.StatusEffects;
using LBoL.Core.Battle.BattleActions;

namespace Momiji.Source
{
    public sealed class CleavingSwipeDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();
            config.GunName = GunNameID.GetGunFromId(400);

            config.Colors = new List<ManaColor>() { ManaColor.White, ManaColor.Red};
            config.Cost = new ManaGroup() { Any = 1, White = 1, Red = 1 };
            config.UpgradedCost = new ManaGroup() {  White = 1, Red = 1 };
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Attack;
            //TargetType.AllEnemies will change the selector to all enemies for attacks/status effects.
            config.TargetType = TargetType.AllEnemies;

            config.Damage = 10;
            config.UpgradedDamage = 15;

            config.Value1 = 2;
            config.UpgradedValue1 = 2;

            //Add Lock On descrption when hovering over the card.


            config.Illustrator = "hide448";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(CleavingSwipeDef))]
    public sealed class CleavingSwipe : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            //Attack all enemies, selector is set to Battle.AllAliveEnemies.
            yield return AttackAction(selector, GunName);
            //If the battle were to end, skip the rest of the method.
            if (Battle.BattleShouldEnd)
            {
                yield break;
            }
            //Draw cards immediately.
            else
            {
                yield return new DrawManyCardAction(base.Value1);
            }
            yield break;
        }
    }
}


