using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using Momiji.Source;
using Momiji.Source.GunName;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.StatusEffects;
using LBoL.Core.Cards;
using System.Linq;
using LBoL.Core.Battle.Interactions;
using Momiji.Source.BattleActions;
using LBoL.EntityLib.StatusEffects.Basic;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Units;

namespace Momiji.Source.Cards
{
    public sealed class LookoutsIntuitionDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();

            config.Colors = new List<ManaColor>() { ManaColor.Red };
            config.Cost = new ManaGroup() { Any = 2, Red = 1 };
            config.UpgradedCost = new ManaGroup() { Any = 1, Red = 1 };
            config.Rarity = Rarity.Common;

            config.Type = CardType.Skill;
            config.TargetType = TargetType.SingleEnemy;

            config.Value1 = 2;

            config.Value2 = 3;
            config.UpgradedValue2 = 2;
            config.Shield = 3;
            config.UpgradedShield = 3;

            config.RelativeEffects = new List<string>() { nameof(Vulnerable), nameof(Graze) };
            config.UpgradedRelativeEffects = new List<string>() { nameof(Vulnerable), nameof(Graze) };

            config.Illustrator = "Shirokaba114";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }
    
    [EntityLogic(typeof(LookoutsIntuitionDef))]
    public sealed class LookoutsIntuition : SampleCharacterCard
    {

        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            int count = 0;
            EnemyUnit selectedEnemy = selector.SelectedEnemy;
            yield return base.BuffAction<Graze>(base.Value1, 0, 0, 0, 0.2f);
            if(selectedEnemy.HasStatusEffect<Vulnerable>())
            {
                count = (selectedEnemy.GetStatusEffect<Vulnerable>().Duration) / base.Value2;
                yield return new CastBlockShieldAction(base.Battle.Player, 0, count * base.Shield.Shield, BlockShieldType.Direct, false);
            }
            yield break;
        }
    }
}


