using LBoL.Base;
using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using Momiji.Source;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.Battle.BattleActions;
using LBoL.EntityLib.StatusEffects.Basic;
using Momiji.Source.StatusEffects;

namespace Momiji.Source.Cards
{
    public sealed class PlungePoolDef : SampleCharacterCardTemplate
    {


        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();

            config.Colors = new List<ManaColor>() { ManaColor.Blue };
            config.Cost = new ManaGroup() { Any = 2, Blue = 1 };
            config.UpgradedCost = new ManaGroup() { Any = 2, Blue = 1 };
            config.Rarity = Rarity.Uncommon;

            config.Type = CardType.Defense;
            config.TargetType = TargetType.Self;

            config.Block = 13;
            config.UpgradedBlock = 0;

            config.Shield = 0;
            config.UpgradedShield = 13;

            config.RelativeEffects = new List<string>() { nameof(RetaliationSe) };

            config.Illustrator = "Oba";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(PlungePoolDef))]
    public sealed class PlungePool : SampleCharacterCard
    {
        public int ReflectShield
        {
            get
            {
                if (base.Battle != null && base.Battle.Player.HasStatusEffect<RetaliationSe>())
                {
                    return base.Battle.Player.GetStatusEffect<RetaliationSe>().Level;
                }
                return 0;
            }
        }
        public override int AdditionalBlock
        {
            get
            {
                if (base.Battle != null && !this.IsUpgraded)
                {
                    return this.ReflectShield;
                }
                return 0;
            }
        }

        public override int AdditionalShield
        {
            get
            {
                if (base.Battle != null && this.IsUpgraded)
                {
                    return this.ReflectShield;
                }
                return 0;
            }
        }
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {

        yield return new CastBlockShieldAction(Battle.Player, base.Block.Block, base.Shield.Shield, BlockShieldType.Normal, true);
            //This is equivalent to:
            //yield return new CastBlockShieldAction(Battle.Player, base.Block, base.Shield, BlockShieldType.Normal, true); 
            yield break;
        }
    }
}