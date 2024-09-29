using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using Momiji.Source;
using Momiji.Source.StatusEffects;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.Units;
using LBoL.Core.Battle.BattleActions;
using LBoL.EntityLib.StatusEffects.Basic;
using System.Linq;
using LBoL.Core.Intentions;
using LBoL.EntityLib.StatusEffects.Cirno;

namespace Momiji.Source.Cards
{
    public sealed class FrigidSkyDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();
            config.Colors = new List<ManaColor>() { ManaColor.Blue };
            config.Cost = new ManaGroup() { Any = 3, Blue = 1 };
            config.UpgradedCost = new ManaGroup() { Any = 2, Blue = 1 };
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Ability;
            config.TargetType = TargetType.Nobody;

            config.Value1 = 1;
            config.UpgradedValue1 = 1;

            config.Value2 = 1;
            config.UpgradedValue2 = 2;

            config.Illustrator = "ètè∫";

            config.RelativeEffects = new List<string>() { nameof(Cold) };
            config.UpgradedRelativeEffects = new List<string>() { nameof(Cold) };

            config.RelativeCards = new List<string>() { nameof(AirCutter) };
            config.UpgradedRelativeCards = new List<string>() { nameof(AirCutter) };

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }
    
    [EntityLogic(typeof(FrigidSkyDef))]
    public sealed class FrigidSky : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            yield return new ApplyStatusEffectAction<FrigidSkySe>(Battle.Player, base.Value1, 0, 0, 0, 0.2f);
            yield return new AddCardsToHandAction(Library.CreateCards<AirCutter>(base.Value2, false));
            yield break;
        }
    }
}


