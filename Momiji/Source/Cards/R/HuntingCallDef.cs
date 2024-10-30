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
using Momiji.Source.StatusEffects;

namespace Momiji.Source.Cards
{
    public sealed class HuntingCallDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();

            config.Colors = new List<ManaColor>() { ManaColor.Red };
            config.Cost = new ManaGroup() { Red = 2 };
            config.UpgradedCost = new ManaGroup() { Any = 1, Red = 1 };
            config.Rarity = Rarity.Common;

            config.Type = CardType.Skill;
            config.TargetType = TargetType.AllEnemies;

            config.Value1 = 2;

            config.Value2 = 2;
            config.UpgradedValue2 = 3;

            config.RelativeEffects = new List<string>() { nameof(Vulnerable), nameof(OffensiveIntention) };
            config.UpgradedRelativeEffects = new List<string>() { nameof(Vulnerable), nameof(OffensiveIntention) };

            config.Illustrator = "黒てー";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(HuntingCallDef))]
    public sealed class HuntingCall : SampleCharacterCard
    {

        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            int intention = 0;
            int count = 0;
            EnemyUnit[] enemies = selector.GetEnemies(base.Battle);
            foreach (EnemyUnit enemyUnit in enemies)
            {
                yield return new ApplyStatusEffectAction<Vulnerable>(enemyUnit, 0, base.Value1, 0, 0, 0.2f);
                intention = base.IntentionCheck(enemyUnit);
                if (intention == 1 || intention == 3 || intention == 5 || intention == 7)
                {
                    count++;
                }
            }
            if (count > 0)
            {
                yield return new DrawManyCardAction(base.Value2);
            }
        }
    }
}