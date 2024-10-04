using LBoL.Base;
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
using LBoL.Core.Units;

namespace Momiji.Source.Cards
{
    public sealed class AlwaysWatchingDef : SampleCharacterCardTemplate
    {


        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();

            config.Colors = new List<ManaColor>() { ManaColor.White };
            config.Cost = new ManaGroup() { Any = 2, White = 1 };
            config.UpgradedCost = new ManaGroup() { Any = 2, White = 1 };
            config.Rarity = Rarity.Rare;

            config.Type = CardType.Defense;
            config.TargetType = TargetType.Self;

            config.Block = 15;
            config.UpgradedBlock = 18;

            config.Shield = 15;
            config.UpgradedShield = 18;

            config.Value1 = 2;
            config.UpgradedValue1 = 3;

            config.Illustrator = "";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(AlwaysWatchingDef))]
    public sealed class AlwaysWatching : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            int intention = 0;
            int count = 0;
            EnemyUnit[] enemies = selector.GetEnemies(base.Battle);
            yield return new CastBlockShieldAction(base.Battle.Player, base.Battle.Player, 0,  base.Shield.Shield, BlockShieldType.Normal, true);
            foreach (EnemyUnit enemyUnit in enemies)
            {
                intention = base.IntentionCheck(enemyUnit);
                if (intention == 1 || intention == 3 || intention ==5 || intention == 7)
                {
                    count++;
                }
            }
            if (count > 0)
            {
                yield return new CastBlockShieldAction(base.Battle.Player, base.Battle.Player, base.Block.Block, 0, BlockShieldType.Normal, true);
            }
            yield break;
            //This is equivalent to:
            //yield return new CastBlockShieldAction(Battle.Player, base.Block, base.Shield, BlockShieldType.Normal, true);
        }
    }
}