using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using Momiji.Source;
using Momiji.Source.StatusEffects;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;

namespace Momiji.Source.Cards
{
    public sealed class WaterfallMeditationDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();

            config.Colors = new List<ManaColor>() { ManaColor.White };
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
            config.Cost = new ManaGroup() { White = 1 };
            config.UpgradedCost = new ManaGroup() { White = 1 };
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Skill;
            config.TargetType = TargetType.SingleEnemy;

            config.Value1 = 1;
            config.UpgradedValue1 = 2;
            config.Value2 = 6;
            config.UpgradedValue2 = 9;

            config.RelativeEffects = new List<string>() {  nameof(Vulnerable), nameof(SpiritNegative), nameof(Firepower) };
            config.UpgradedRelativeEffects = new List<string>() {  nameof(Vulnerable), nameof(SpiritNegative), nameof(Firepower) };

            config.Illustrator = "esai";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(WaterfallMeditationDef))]
    public sealed class WaterfallMeditation : SampleCharacterCard
    {


        // Token: 0x060009C6 RID: 2502 RVA: 0x00014544 File Offset: 0x00012744
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            EnemyUnit selectedEnemy = selector.SelectedEnemy;
            int intention = base.IntentionCheck(selectedEnemy);
            if (intention == 1 || intention == 3 || intention == 5 || intention == 7)
            {
                yield return new CastBlockShieldAction(base.Battle.Player, base.Battle.Player, base.Value2, 0, BlockShieldType.Normal, true);
                yield return base.UpgradeRandomHandAction(base.Value1, CardType.Attack); 
            }
            if (intention == 2 || intention == 3 || intention == 6)
            {
                yield return new ApplyStatusEffectAction<Vulnerable>(selectedEnemy, 0, base.Value1, 0, 0, 0.2f);
                yield return new ApplyStatusEffectAction<SpiritNegative>(selectedEnemy, 0, base.Value1, 0, 0, 0.2f);
            }
            if (intention >= 4)
            { 
                yield return new ApplyStatusEffectAction<Firepower>(base.Battle.Player, base.Value1, 0, 0, 0, 0.2f); 
            }
            yield return new DrawManyCardAction(base.Value1);
        }
    }
}