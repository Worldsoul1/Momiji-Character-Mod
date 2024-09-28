using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using Momiji.Source;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.Battle.BattleActions;
using LBoL.EntityLib.JadeBoxes;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using Momiji.Source.StatusEffects;

namespace Momiji.Source.Cards.R
{
    public sealed class BladeWardingDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();
            config.Colors = new List<ManaColor>() { ManaColor.Red };
            config.Cost = new ManaGroup() { Red = 1 };
            config.UpgradedCost = new ManaGroup() { Any = 1 };
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Skill;
            config.TargetType = TargetType.SingleEnemy;

            config.Value1 = 2;
            config.UpgradedValue1 = 2;
            config.Value2 = 1;
            config.UpgradedValue2 = 1;

            config.Illustrator = "";

            config.Keywords = Keyword.Exile;
            config.UpgradedKeywords = Keyword.Exile;

            config.RelativeEffects = new List<string>() { nameof(Weak), nameof(RetaliationSe) };
            config.UpgradedRelativeEffects = new List<string>() { nameof(Weak), nameof(RetaliationSe) };

            config.RelativeCards = new List<string>() { nameof(AirCutter) };
            config.UpgradedRelativeCards = new List<string>() { nameof(AirCutter) };

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }
    [EntityLogic(typeof(BladeWardingDef))]
    public sealed class BladeWarding : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            EnemyUnit selectedEnemy = selector.SelectedEnemy;
            //Buff and debuff have either a level or a duration.
            //Duration: Effects that last a certain amount of turns then disappear.
            //Level: Effects that have a fixed duration but that can vary in intensity. 
            //Weak and Vulnerable have a duration, FirepowerNegative has a level.  
            //DebuffAction's 2nd field is the level, the 3rd one is the duration.
            yield return base.DebuffAction<Weak>(selectedEnemy, 0, base.Value1, 0, 0, true, 0.2f);
            yield return new ApplyStatusEffectAction<RetaliationSe>(Battle.Player, base.Value1, 0, 0, 0, 0.2f);
            yield return new AddCardsToHandAction(Library.CreateCards<AirCutter>(base.Value2, false));
            yield break;
        }
    }
}

