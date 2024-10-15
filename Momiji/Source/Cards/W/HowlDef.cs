using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.Intentions;
using LBoL.EntityLib.StatusEffects.Basic;
using LBoLEntitySideloader.Attributes;
using System;
using LBoL.Core.Battle.BattleActions;
using System.Collections.Generic;
using System.Text;
using LBoL.Core.Units;
using System.Linq;
using LBoL.Core.StatusEffects;

namespace Momiji.Source.Cards
{
    public sealed class HowlDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();

            config.Colors = new List<ManaColor>() { ManaColor.White };
            config.Cost = new ManaGroup() { White = 1 };
            config.UpgradedCost = new ManaGroup() { Any = 1 };
            config.Rarity = Rarity.Common;

            config.Type = CardType.Skill;
            config.TargetType = TargetType.SingleEnemy;

            config.Value1 = 2;
            config.UpgradedValue1 = 3;

            config.Keywords = Keyword.Exile;
            config.UpgradedKeywords = Keyword.Exile;

            config.RelativeEffects = new List<string>() { nameof(FirepowerNegative) };
            config.UpgradedRelativeEffects = new List<string>() { nameof(FirepowerNegative) };

            config.Illustrator = "Belderchal";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(HowlDef))]
    public sealed class Howl : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            EnemyUnit selectedEnemy = selector.SelectedEnemy;
            int count = base.IntentionCheck(selectedEnemy);
            if (count == 1 || count == 3 || count == 5 || count == 7)
            {
                yield return new ApplyStatusEffectAction<FirepowerNegative>(selectedEnemy, base.Value1, 0, 0, 0, 0.2f);
            }
            yield break;
        }
    }
}