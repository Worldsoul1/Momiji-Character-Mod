using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using Momiji.Source;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core;
using LBoL.Core.StatusEffects;
using Momiji.Source.StatusEffects;

namespace Momiji.Source.Cards
{
    public sealed class MapleLeafFestivalDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();
            config.Colors = new List<ManaColor>() { ManaColor.White, ManaColor.Red };
            config.Cost = new ManaGroup() { Any = 1, Hybrid = 1, HybridColor = 2 };
            config.UpgradedCost = new ManaGroup() { Any = 1 };
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Ability;
            config.TargetType = TargetType.Nobody;

            config.Value1 = 1;

        config.RelativeCards = new List<string>() { "MapleLeaf" };
        config.UpgradedRelativeCards = new List<string>() { "MapleLeaf" };

            config.Illustrator = "";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }
    
    [EntityLogic(typeof(MapleLeafFestivalDef))]
    public sealed class MapleLeafFestival : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            yield return new ApplyStatusEffectAction<MapleLeafFestivalSe>(Battle.Player, base.Value1, 0, 0, 0, 0.2f);
            //This is equivalent to:
            //yield return new ApplyStatusEffectAction<Firepower>(Battle.Player, base.Value1, 0, 0, 0, 0.2f);
            //yield return new ApplyStatusEffectAction<Spirit>(Battle.Player, base.Value2, 0, 0, 0, 0.2f);
            yield break;
        }
    }
}


