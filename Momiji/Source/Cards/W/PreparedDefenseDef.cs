using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using Momiji.Source;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.Battle.BattleActions;
using LBoL.EntityLib.StatusEffects.Basic;
using Unity.IO.LowLevel.Unsafe;

namespace Momiji.Source.Cards
{
    public sealed class PreparedDefenseDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();

            config.Colors = new List<ManaColor>() { ManaColor.White };
            config.Cost = new ManaGroup() { White = 1 };
            config.UpgradedCost = new ManaGroup() { Any = 1 };
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Skill;
            config.TargetType = TargetType.Self;

            config.Value1 = 4;
            config.UpgradedValue1 = 6;

            config.RelativeEffects = new List<string>() { nameof(Reflect), nameof(TempElectric) };

            config.Illustrator = "";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(PreparedDefenseDef))]
    public sealed class PreparedDefense : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            yield return base.BuffAction<Reflect>(base.Value1, 0, 0, 0, 0.2f);
            int tempelec = base.Battle.Player.GetStatusEffect<Reflect>().Level;
            yield return new RemoveStatusEffectAction(Battle.Player.GetStatusEffect<Reflect>());
            yield return base.BuffAction<TempElectric>(tempelec, 0, 0, 0, 0.2f);
            yield break;
        }
    }
}
