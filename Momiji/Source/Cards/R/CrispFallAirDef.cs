using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using Momiji.Source;
using Momiji.Source.StatusEffects;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.Battle.BattleActions;
using LBoL.EntityLib.JadeBoxes;

namespace Momiji.Source.Cards
{
    public sealed class CrispFallAirDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();
            config.Colors = new List<ManaColor>() { ManaColor.Red };
            config.Cost = new ManaGroup() { Any = 2, Red = 1 };
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Skill;
            config.TargetType = TargetType.Nobody;

            config.Value1 = 2;
            config.UpgradedValue1 = 2;

            config.Mana = new ManaGroup() { Red = 1 };
            config.UpgradedMana = new ManaGroup() { Red = 1 };

            config.Illustrator = "Cube85";

            config.RelativeCards = new List<string>() { nameof(AirCutter) };
            config.UpgradedRelativeCards = new List<string>() { nameof(AirCutter) };

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(CrispFallAirDef))]
    public sealed class CrispFallAir : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            //Add a token card to the hand.
            if (!this.IsUpgraded)
            {
                yield return new AddCardsToHandAction(Library.CreateCards<AirCutter>(Value1, false));
                yield return new GainTurnManaAction(base.Mana);
            }
            else
            {
                yield return new AddCardsToHandAction(Library.CreateCards<AirCutter>(Value1, false));
                yield return new GainManaAction(Mana);
                yield return new GainTurnManaAction(base.Mana);
                yield return new ApplyStatusEffectAction<CrispFallAirSe>(Battle.Player, base.Value1, 1, 0, 0, 0.2f);
            }
            yield break;
        }
    }
}
