﻿using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using Momiji.Source;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.StatusEffects;
using LBoL.EntityLib.StatusEffects.Basic;
using LBoL.EntityLib.StatusEffects.Cirno;

namespace Momiji.Source.Cards
{
    public sealed class RagingWaterfallDef : SampleCharacterCardTemplate
    {


        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();

            config.Colors = new List<ManaColor>() { ManaColor.White };
            config.Cost = new ManaGroup() { Any = 2, White = 2 };
            config.UpgradedCost = new ManaGroup() { Any = 3, White = 1 };
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Defense;
            config.TargetType = TargetType.Self;

            config.Block = 24;
            config.UpgradedBlock = 30;

            config.Value1 = 2;
            config.UpgradedValue1 = 3;

            config.Illustrator = "";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(RagingWaterfallDef))]
    public sealed class RagingWaterfall : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            yield return DefenseAction(true);
            yield return base.BuffAction<ExtraDraw>(base.Value1, 0, 0, 0, 0.2f);
            yield break;
            //This is equivalent to:
            //yield return new CastBlockShieldAction(Battle.Player, base.Block, base.Shield, BlockShieldType.Normal, true);
        }
    }
}