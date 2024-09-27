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
    public sealed class SeizeTheMomentDef : SampleCharacterCardTemplate
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
            config.Cost = new ManaGroup() { Any = 1, White = 2 };
            config.UpgradedCost = new ManaGroup() { Any = 1, White = 1 };
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Attack;
            config.TargetType = TargetType.SingleEnemy;

            config.Damage = 16;
            config.UpgradedDamage = 16;

            config.Value1 = 3;
            config.UpgradedValue1 = 2;
            config.Value2 = 1;
            config.UpgradedValue2 = 2;

            config.Keywords = Keyword.Exile;
            config.UpgradedKeywords = Keyword.Exile | Keyword.Accuracy;

            config.RelativeEffects = new List<string>() { nameof(TempFirepowerNegative), nameof(Vulnerable), nameof(Fragil) };
            config.UpgradedRelativeEffects = new List<string>() { nameof(TempFirepowerNegative), nameof(Vulnerable), nameof(Fragil) };

            config.Illustrator = "かなめや";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(SeizeTheMomentDef))]
    public sealed class SeizeTheMoment : SampleCharacterCard
    {
        

        // Token: 0x060009C6 RID: 2502 RVA: 0x00014544 File Offset: 0x00012744
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            EnemyUnit selectedEnemy = selector.SelectedEnemy;
            yield return base.AttackAction(selector, base.GunName);
            int intention = base.IntentionCheck(selectedEnemy);
            if (intention == 1 || intention == 3 || intention == 5)
                yield return new ApplyStatusEffectAction<TempFirepowerNegative>(selectedEnemy, base.Value1, 0, 0, 0, 0.2f);
            if (intention == 2 || intention == 3 || intention == 6)
                yield return new ApplyStatusEffectAction<Vulnerable>(selectedEnemy, base.Value1, 0, 0, 0, 0.2f);
                yield return new ApplyStatusEffectAction<Fragil>(selectedEnemy, base.Value1, 0, 0, 0, 0.2f);
            if (intention >= 4)
                yield return base.AttackAction(selector, base.GunName);

        }
    }
}