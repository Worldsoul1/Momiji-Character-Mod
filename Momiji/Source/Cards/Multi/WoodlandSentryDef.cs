using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using Momiji.Source;
using Momiji.Source.GunName;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.Battle.BattleActions;
using Momiji.Source.StatusEffects;

namespace Momiji.Source.Cards
{
    public sealed class WoodlandSentryDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();
            config.GunName = GunNameID.GetGunFromId(400);

            config.Colors = new List<ManaColor>() { ManaColor.White, ManaColor.Green };
            config.Cost = new ManaGroup() { White = 1, Green = 1 };
            config.Rarity = Rarity.Uncommon;


            config.Type = CardType.Ability;
            config.TargetType = TargetType.Nobody;

            config.Value1 = 1;
            config.UpgradedValue1 = 2;

            config.Value2 = 3;

            config.Illustrator = "";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }
    
    [EntityLogic(typeof(WoodlandSentryDef))]
    public sealed class WoodlandSentry : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            yield return base.HealAction(base.Value2);
            yield return new ApplyStatusEffectAction<WoodlandSentrySe>(base.Battle.Player, base.Value1, 0, 0, 0, 0.2f);
            yield break;
        }
    }
}


