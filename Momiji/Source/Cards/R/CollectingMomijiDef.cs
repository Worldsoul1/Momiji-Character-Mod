using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using Momiji.Source;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.Battle.BattleActions;
using LBoL.EntityLib.JadeBoxes;

namespace Momiji.Source
{
    public sealed class CollectingMomijiDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();
            config.Colors = new List<ManaColor>() { ManaColor.Red };
            config.Cost = new ManaGroup() { Red = 1 };
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Skill;
            config.TargetType = TargetType.Nobody;

            config.Value1 = 2;
            config.UpgradedValue1 = 2;

            config.Illustrator = "en (shihi no utage)";

            config.RelativeCards = new List<string>() { nameof(MapleLeaf) };
            config.UpgradedRelativeCards = new List<string>() { nameof(MapleLeaf) };

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(CollectingMomijiDef))]
    public sealed class CollectingMomiji : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            //Add a token card to the hand.
            if (!this.IsUpgraded)
            {
                yield return new AddCardsToHandAction(Library.CreateCards<MapleLeaf>(Value1, false));
            }
            else
            {
                yield return new AddCardsToHandAction(Library.CreateCards<MapleLeaf>(Value1, true));
            }
            yield break;
        }
    }
}


