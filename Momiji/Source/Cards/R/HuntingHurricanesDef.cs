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
using LBoL.EntityLib.JadeBoxes;
using System.Linq;

namespace Momiji.Source.Cards
{
    public sealed class HuntingHurricanesDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();

            config.GunName = GunNameID.GetGunFromId(400);

            config.Colors = new List<ManaColor>() { ManaColor.Red };
            config.Cost = new ManaGroup() { Any = 2, Red = 2 };
            config.UpgradedCost = new ManaGroup() { Any = 2, Red = 1 };
            config.Rarity = Rarity.Rare;

            config.Type = CardType.Attack;
            config.TargetType = TargetType.AllEnemies;

            config.Damage = 10;
            config.UpgradedDamage = 10;

            config.Value1 = 10;

            //The Accuracy keyword is enough to make an attack accurate.
            config.Keywords = Keyword.Accuracy;
            config.UpgradedKeywords = Keyword.Accuracy;

            config.RelativeCards = new List<string>() { nameof(AirCutter)};
            config.UpgradedRelativeCards = new List<string>() { nameof(AirCutter) };

            config.Illustrator = "まだら鳩";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }
    [EntityLogic(typeof(HuntingHurricanesDef))]
    public sealed class HuntingHurricanes : SampleCharacterCard
    {
        //By default, if config.Damage / config.Block / config.Shield are set:
        //The card will deal damage or gain Block/Barrier without having to set anything.
        //Here, this is is equivalent to the following code.
        int AirCutterCount
        {
            get
            {
                if (base.Battle != null)
                {
                    return base.Battle.ExileZone.OfType<AirCutter>().Count<AirCutter>();
                }
                else
                {
                    return 0;
                }
            }
        }
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {

                yield return base.AttackAction(selector, GunName);
            for (int i = 0; i < AirCutterCount; i++) 
            {
                yield return base.AttackAction(selector, GunName);
            }

        }
    }
}

