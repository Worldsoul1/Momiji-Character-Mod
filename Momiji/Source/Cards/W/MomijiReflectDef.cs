using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using Momiji.Source;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.Units;
using LBoL.Core.Battle.BattleActions;

namespace Momiji.Source.Cards
{
    public sealed class MomijiReflectDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();
            config.Colors = new List<ManaColor>() { ManaColor.White };
            config.Cost = new ManaGroup() { Any=1, White = 1 };
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Defense;
            config.TargetType = TargetType.Self;

            config.Shield = 10;
            config.UpgradedShield = 12;

            config.Value1 = 4;
            config.UpgradedValue1 = 6;

            config.Keywords = Keyword.Exile;
            config.UpgradedKeywords = Keyword.Exile;
            config.Illustrator = "EKOR";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }
    
    [EntityLogic(typeof(MomijiReflectDef))]
    public sealed class MomijiReflect : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            EnemyUnit selectedEnemy = selector.SelectedEnemy;
            //The player loses life:
            yield return HealAction(base.Value1);
            yield return base.DefenseAction(true);
            //This is equvalent to:
            //yield return new HealAction(Battle.Player, selectedEnemy, base.Value1);

            //Target enemy loses hp.
            yield break;
        }
    }
}


