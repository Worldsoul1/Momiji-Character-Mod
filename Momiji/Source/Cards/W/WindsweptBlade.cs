using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using Momiji.Source;
using Momiji.Source.GunName;
using Momiji.Source.StatusEffects;

namespace Momiji.Source.Cards
{
    public sealed class WindsweptBladeDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();
            config.GunName = GunNameID.GetGunFromId(400);

            config.Colors = new List<ManaColor>() { ManaColor.White };
            config.Cost = new ManaGroup() { Any = 2, White = 1 };
            config.UpgradedCost = new ManaGroup() { Any = 2, White = 1 };
            config.Rarity = Rarity.Common;

            config.Type = CardType.Attack;
            config.TargetType = TargetType.SingleEnemy;

            config.Damage = 16;
            config.UpgradedDamage = 21;

            config.Value1 = 5;
            config.Value2 = 8;

            config.RelativeEffects = new List<string>() { nameof(RetaliationSe) };
            config.UpgradedRelativeEffects = new List<string>() { nameof(RetaliationSe) };

            config.Keywords = Keyword.Accuracy;
            config.UpgradedKeywords = Keyword.Accuracy;

            config.Illustrator = "Wholesome_illustrator";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(WindsweptBladeDef))]
    public sealed class WindsweptBlade : SampleCharacterCard
    {
        //By default, if config.Damage / config.Block / config.Shield are set:
        //The card will deal damage or gain Block/Barrier without having to set anything.
        //Here, this is is equivalent to the following code.

        
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            yield return base.AttackAction(selector, base.GunName);
            yield return new ApplyStatusEffectAction<RetaliationSe>(base.Battle.Player, base.Value1, 0, 0, 0, 0.2f);
            yield break;
        }
    }
}