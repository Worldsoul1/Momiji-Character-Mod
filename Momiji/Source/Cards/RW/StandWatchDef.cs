using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using Momiji.Source;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core;
using LBoL.Core.Units;
using LBoL.Core.StatusEffects;
using Momiji.Source.StatusEffects;

namespace Momiji.Source.Cards
{
    public sealed class StandWatchDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();
            config.Colors = new List<ManaColor>() { ManaColor.White, ManaColor.Red };
            config.Cost = new ManaGroup() { Any = 1, White = 1, Red = 1 };
            config.UpgradedCost = new ManaGroup() { White = 1, Red = 1 };
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Ability;
            config.TargetType = TargetType.Self;

            config.Block = 10;
            config.UpgradedBlock = 15;
            config.Shield = 0;
            config.UpgradedShield = 0;
            config.Value1 = 5;
            config.UpgradedValue1 = 5;
            config.Value2 = 4;
            config.UpgradedValue2 = 4;

            config.Illustrator = "スコッティ";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(StandWatchDef))]
    public sealed class StandWatch : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            yield return new CastBlockShieldAction(base.Battle.Player, base.Block.Block, base.Shield.Shield, BlockShieldType.Normal, true);
            yield return new ApplyStatusEffectAction<StandWatchSe>(Battle.Player, base.Value1, 0, 0, 0, 0.2f);
            yield return new ApplyStatusEffectAction<TurnStartDontLoseBlock>(Battle.Player, base.Value2, 0, 0, 0, 0.2f);
            //This is equivalent to:
            //yield return new ApplyStatusEffectAction<Firepower>(Battle.Player, base.Value1, 0, 0, 0, 0.2f);
            //yield return new ApplyStatusEffectAction<Spirit>(Battle.Player, base.Value2, 0, 0, 0, 0.2f);
            yield break;
        }
    }
}