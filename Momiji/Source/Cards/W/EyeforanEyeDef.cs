using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core.Battle;
using LBoL.Core.Cards;
using LBoL.Core;
using LBoLEntitySideloader.Attributes;
using Momiji.Source.GunName;
using System;
using System.Collections.Generic;
using System.Text;
using LBoL.EntityLib.StatusEffects.Basic;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Units;


namespace Momiji.Source.Cards
{
    public sealed class EyeforanEyeDef : SampleCharacterCardTemplate
    {
        public override CardConfig MakeConfig()
        {
            CardConfig config = GetCardDefaultConfig();

            config.GunName = GunNameID.GetGunFromId(400);

            config.Colors = new List<ManaColor>() { ManaColor.White };
            config.Cost = new ManaGroup() {White = 1 };
            config.UpgradedCost = new ManaGroup() { Any = 1};
            config.Rarity = Rarity.Common;

            config.Type = CardType.Attack;
            config.TargetType = TargetType.SingleEnemy;

            config.Damage = 8;
            config.UpgradedDamage = 10;

            config.Value1 = 4;
            config.UpgradedValue1 = 2;


            //The Accuracy keyword is enough to make an attack accurate.

            config.Illustrator = "銀朱";

            config.Index = CardIndexGenerator.GetUniqueIndex(config);
            return config;
        }
    }

    [EntityLogic(typeof(EyeforanEyeDef))]
    public sealed class EyeforanEye : SampleCharacterCard
    {
        //By default, if config.Damage / config.Block / config.Shield are set:
        //The card will deal damage or gain Block/Barrier without having to set anything.
        //Here, this is is equivalent to the following code.
        protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
        {
            EnemyUnit selectedEnemy = selector.SelectedEnemy;
            yield return base.AttackAction(selector, base.GunName);
            if(!selectedEnemy.IsAlive) { yield break; }
            yield return new DamageAction(selectedEnemy, Battle.Player, DamageInfo.Attack(base.Value1));
        }
    }
}