using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using Momiji.Source;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;

namespace Momiji.Source.Cards
{
    public sealed class RipApartDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();

            config.Colors = new List<ManaColor>() { ManaColor.White, ManaColor.Red };
            //Hybrid colors:
            //0 = W/U
            //1 = W/B
            //2 = W/R
            //3 = W/G
            //4 = U/B
            //5 = U/R
            //6 = U/G
            //7 = B/R
            //8 = B/G
            //9 = R/G
            //As of 1.5.1: Colorless hybrid are not supported.    
            config.Cost = new ManaGroup() { Hybrid = 2, HybridColor = 2};
            config.UpgradedCost = new ManaGroup() { Any = 1, Hybrid = 1, HybridColor = 2 };
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Attack;
            config.TargetType = TargetType.AllEnemies;

            config.Damage = 8;
            config.UpgradedDamage = 11;

            config.Value1 = 1;
            config.Value2 = 2;

            config.RelativeEffects = new List<string>() { nameof(Vulnerable)};
            config.UpgradedRelativeEffects = new List<string>() { nameof(Vulnerable)};

            config.Illustrator = "ゴ太郎　(ゴミ豚クズ太郎）";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }
    
    [EntityLogic(typeof(RipApartDef))]
    public sealed class RipApart : SampleCharacterCard
    {
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            yield return base.AttackAction(selector, base.GunName);
            if (base.Battle.BattleShouldEnd)
            {
                yield break;
            }
            foreach (BattleAction battleAction in base.DebuffAction<Vulnerable>(base.Battle.AllAliveEnemies, 0, base.Value1, 0, 0, true, 0.03f))
            {
                yield return battleAction;
            }
            yield break;
        }
    }
}


