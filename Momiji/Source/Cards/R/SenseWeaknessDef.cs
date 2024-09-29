using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using Momiji.Source;
using Momiji.Source.StatusEffects;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoL.Core.Battle.BattleActions;
using LBoL.EntityLib.StatusEffects.Basic;
using System.Linq;
using LBoL.Core.Intentions;
using LBoL.EntityLib.StatusEffects.Cirno;

namespace Momiji.Source.Cards
{
    public sealed class SenseWeaknessDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();
            config.Colors = new List<ManaColor>() { ManaColor.Red };
            config.Cost = new ManaGroup() { Red = 3};
            config.UpgradedCost = new ManaGroup() { Red = 2};
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Ability;
            config.TargetType = TargetType.Nobody;

            config.Value1 = 2;
            config.UpgradedValue1 = 2;

            config.Value2 = 25;
            config.UpgradedValue2 = 25;

            config.Illustrator = "のぎぐち";

            config.RelativeEffects = new List<string>() { nameof(Vulnerable) };
            config.UpgradedRelativeEffects = new List<string>() { nameof(Vulnerable) };

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(SenseWeaknessDef))]
    public sealed class SenseWeakness : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            yield return new ApplyStatusEffectAction<SenseWeaknessSe>(Battle.Player, base.Value2, 0, 0, 0, 0.2f);
            foreach (BattleAction battleAction in base.DebuffAction<Vulnerable>(base.Battle.AllAliveEnemies, 0, base.Value1, 0, 0, true, 0.2f))
            {
                yield return battleAction;
            }
            yield break;
        }
    }
}