using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using Momiji.Source;
using Momiji.Source.GunName;
using Momiji.Source.StatusEffects;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.StatusEffects;
using LBoL.Core.Cards;
using System.Linq;
using LBoL.Core.Battle.Interactions;
using Momiji.Source.BattleActions;
using LBoL.EntityLib.StatusEffects.Basic;
using LBoL.Core.Units;
using LBoL.Core.Battle.BattleActions;

namespace Momiji.Source.Cards
{
    public sealed class PartingSliceDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();

            config.Colors = new List<ManaColor>() { ManaColor.White, ManaColor.Red };
            config.Cost = new ManaGroup() { Any = 1, White = 1, Red = 1 };
            config.Rarity = Rarity.Rare;

            config.Type = CardType.Attack;
            config.TargetType = TargetType.AllEnemies;

            config.Damage = 18;
            config.UpgradedDamage = 12;

            config.Value1 = 1;
            config.Value2 = 2;

            config.RelativeEffects = new List<string>() { nameof(Graze) };
            config.UpgradedRelativeEffects = new List<string>() { nameof(Graze) };

            config.Illustrator = "竹篙";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(PartingSliceDef))]
    public sealed class PartingSlice : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            int intention = 0;
            int count = 0;
            EnemyUnit[] enemies = selector.GetEnemies(base.Battle);
            foreach (EnemyUnit enemy in enemies)
            {
                intention = base.IntentionCheck(enemy);
                if (intention == 1 || intention == 3 || intention == 5)
                    count++;
            }
            yield return base.AttackAction(selector, base.GunName);
            if (this.IsUpgraded)
            {
                yield return base.AttackAction(selector, base.GunName);
            }
            if (base.Battle.BattleShouldEnd)
            {
                yield break;
            }
            if(count > 0)
            {
                yield return new ApplyStatusEffectAction<Graze>(base.Battle.Player, base.Value1 * count, 0, 0, 0, 0.2f);
            }
            yield break;
        }
    }
}
