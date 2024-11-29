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

namespace Momiji.Source.Cards
{
    public sealed class FarSightDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();

            config.Colors = new List<ManaColor>() { ManaColor.White };
            config.Cost = new ManaGroup() { White = 2 };
            config.UpgradedCost = new ManaGroup() { White = 1 };
            config.Rarity = Rarity.Common;

            config.Type = CardType.Skill;
            config.TargetType = TargetType.AllEnemies;

            config.Block = 12;
            config.UpgradedBlock = 12;

            config.Value1 = 12;
            config.UpgradedValue1 = 12;

            config.RelativeEffects = new List<string>() { nameof(Reflect) };
            config.UpgradedRelativeEffects = new List<string>() { nameof(Reflect) };

            config.Illustrator = "hasebe yuusaku";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(FarSightDef))]
    public sealed class FarSight : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            int attackCount = 0;
                EnemyUnit[] enemies = selector.GetEnemies(base.Battle);
                attackCount = enemies.Count((EnemyUnit enemy) => enemy.Intentions.Any(delegate (Intention i)
                {
                    if (!(i is AttackIntention))
                    {
                        SpellCardIntention spellCardIntention = i as SpellCardIntention;
                        if (spellCardIntention == null || spellCardIntention.Damage == null)
                        {
                            return false;
                        }
                    }
                    return true;
                }));
            
            yield return new ApplyStatusEffectAction<Reflect>(Battle.Player, base.Value1, 0, 0, 0, 0.2f);
            if (base.Battle.Player.HasStatusEffect<Reflect>())
            {
                base.Battle.Player.GetStatusEffect<Reflect>().Gun = (this.IsUpgraded ? "心抄斩B" : "心抄斩");
            }
            if (attackCount > 0)
            {
                yield return base.DefenseAction(true) ;
            }
            //This is equivalent to:
            //yield return new CastBlockShieldAction(Battle.Player, base.Block, base.Shield, BlockShieldType.Normal, true); 
            yield break;
        }
    }
}
