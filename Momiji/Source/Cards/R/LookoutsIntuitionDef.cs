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
            config.TargetType = TargetType.Nobody;

            config.Value1 = 2;

            config.Value2 = 10;
            config.UpgradedValue2 = 10;

            config.RelativeEffects = new List<string>() { nameof(Reflect), nameof(Graze) };

            config.Illustrator = "";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }
    
    [EntityLogic(typeof(LookoutsIntuitionDef))]
    public sealed class LookoutsIntuition : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            yield return base.BuffAction<Graze>(base.Value1, 0, 0, 0, 0.2f);
            yield return base.BuffAction<Reflect>(base.Value2, 0, 0, 0, 0.2f);
            if (base.Battle.Player.HasStatusEffect<Reflect>())
            {
                base.Battle.Player.GetStatusEffect<Reflect>().Gun = (this.IsUpgraded ? "‹à?‘ÌB" : "‹à?‘Ì");
            }
            yield break;
        }
    }
}


