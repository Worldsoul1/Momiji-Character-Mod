﻿using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.Intentions;
using LBoL.EntityLib.StatusEffects.Basic;
using LBoLEntitySideloader.Attributes;
using System;
using LBoL.Core.Battle.BattleActions;
using System.Collections.Generic;
using Momiji.Source.StatusEffects;
using System.Text;
using LBoL.Core.Units;
using System.Linq;

namespace Momiji.Source.Cards
{
    public sealed class TwinFangDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();

            config.Colors = new List<ManaColor>() { ManaColor.White };
            config.Cost = new ManaGroup() { Any = 1, White = 1 };
            config.UpgradedCost = new ManaGroup() { White = 1 };
            config.Rarity = Rarity.Common;

            config.Type = CardType.Skill;
            config.TargetType = TargetType.Self;

            config.Value1 = 1;
            config.UpgradedValue1 = 1;
            config.Value2 = 4;
            config.UpgradedValue2 = 7; 

            config.RelativeEffects = new List<string>() { nameof(Reflect) };
            config.UpgradedRelativeEffects = new List<string>() { nameof(Reflect) }; 

            config.Illustrator = "崩壊";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(TwinFangDef))]
    public sealed class TwinFang : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            yield return new ApplyStatusEffectAction<Reflect>(base.Battle.Player, base.Value2, 0, 0, 0, 0.2f);
            if (base.Battle.Player.HasStatusEffect<Reflect>())
            {
                base.Battle.Player.GetStatusEffect<Reflect>().Gun = (this.IsUpgraded ? "心抄斩B" : "心抄斩");
            }
            yield return new ApplyStatusEffectAction<TwinFangSe>(Battle.Player, base.Value1, 0, 0, 0, 0.2f);
            //This is equivalent to:
            //yield return new CastBlockShieldAction(Battle.Player, base.Block, base.Shield, BlockShieldType.Normal, true); 
            yield break;
        }
    }
}