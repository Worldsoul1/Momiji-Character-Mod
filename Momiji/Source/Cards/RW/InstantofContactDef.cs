using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using Momiji.Source;
using Momiji.Source.StatusEffects;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.StatusEffects;
using LBoL.Core.Battle.BattleActions;
using LBoL.EntityLib.Cards;
using LBoL.EntityLib.StatusEffects.ExtraTurn;

namespace Momiji.Source.Cards
{
    public sealed class InstantofContactDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();
            config.Colors = new List<ManaColor>() { ManaColor.White, ManaColor.Red };
            config.Cost = new ManaGroup() { Any = 3, White = 1, Red = 1 };
            config.UpgradedCost = new ManaGroup() { Any = 2, White = 1, Red = 1 };
            config.Rarity = Rarity.Rare;

            config.Type = CardType.Skill;
            config.TargetType = TargetType.Nobody;

            config.Value1 = 4;
            config.Value2 = 2;
            //Mana config for the Time Limit
            config.Mana = new ManaGroup() { Any = 1 };

            config.RelativeEffects = new List<string>() { nameof(TimeIsLimited), nameof(RetaliationSe) };
            config.UpgradedRelativeEffects = new List<string>() { nameof(TimeIsLimited), nameof(RetaliationSe) };

            config.Illustrator = "";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }
    
    [EntityLogic(typeof(InstantofContactDef))]
    //TimeStopCards inhehit from LimitedStopTimeCard instead of Card
    public sealed class InstantofContact : LimitedStopTimeCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            yield return new ApplyStatusEffectAction<RetaliationSe>(Battle.Player, base.Value1, 0, 0, 0, 0.2f);
            yield return base.BuffAction<ExtraTurn>(1, 0, 0, 0, 0.2f);
            yield return new RequestEndPlayerTurnAction();
            if (base.Limited)
            {
                yield return base.DebuffAction<TimeIsLimited>(base.Battle.Player, 1, 0, 0, 0, true, 0.2f);
                yield return new ApplyStatusEffectAction<InstantofContactSe>(Battle.Player, base.Value2, 1, 0, 0, 0.2f);
            }
            yield break;
        }
    }
}


