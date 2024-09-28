using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using Momiji.Source;
using Momiji.Source.GunName;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.Cards;
using LBoL.Core.Battle.BattleActions;

namespace Momiji.Source.Cards
{
    public sealed class VacuumCutterDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();
            config.GunName = GunNameID.GetGunFromId(400);

            config.Colors = new List<ManaColor>() { ManaColor.White };
            config.Cost = new ManaGroup() { Any = 1, White = 1 };
            config.UpgradedCost = new ManaGroup() { White = 1 };
            config.Rarity = Rarity.Common;

            config.Type = CardType.Attack;
            config.TargetType = TargetType.SingleEnemy;

            config.Damage = 10;
            config.UpgradedDamage = 10;

            config.Value1 = 1;
            config.UpgradedValue1 = 1;

            config.RelativeCards = new List<string>() { nameof(AirCutter) };
            config.UpgradedRelativeCards = new List<string>() { nameof(AirCutter) };

            config.Illustrator = "îLêÖê£";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }
    
    [EntityLogic(typeof(VacuumCutterDef))]
    public sealed class VacuumCutter : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            //Attack the enemy Value1 times. The attack animation is only played once.
            //To play the animation as many time as Value1, set the last parameter of Guns() from false to true.
           
				yield return base.AttackAction(selector, base.GunName);
                yield return new AddCardsToHandAction(Library.CreateCards<AirCutter>(Value1, false));
                yield break;

        }
    }
}


