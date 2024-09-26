using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Battle;
using LBoL.Core.Cards;
using LBoL.Core.Units;
using LBoL.Core;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using Momiji.Source.GunName;
using Momiji.Source.Ultimate;
using LBoL.Core.StatusEffects;
using static UnityEngine.GraphicsBuffer;
//using SampleCharacterMod.BattleActions;

namespace Momiji.Source.Ultimate
{
    public sealed class ExpelleesCananDef : SampleCharacterUltTemplate
    {
        public override UltimateSkillConfig MakeConfig()
        {
            UltimateSkillConfig config = GetDefaulUltConfig();
            config.Damage = 15;
            config.Value1 = 2;
            config.Value2 = 10;
            config.Keywords = Keyword.Accuracy;
            return config;
        }
    }

    [EntityLogic(typeof(ExpelleesCananDef))]
    public sealed class ExpelleesCanan : UltimateSkill
    {
        public BlockInfo Block
        {
            get
            {
                return new BlockInfo(base.Value2, BlockShieldType.Normal);
            }
        }
        public ExpelleesCanan()
        {
            base.TargetType = TargetType.AllEnemies;
            base.GunName = GunNameID.GetGunFromId(4158);
        }

        protected override IEnumerable<BattleAction> Actions(UnitSelector selector)
        {
            Unit[] targets = selector.GetUnits(base.Battle);
            yield return PerformAction.Spell(base.Owner, "Expellee's Canan");
            yield return new CastBlockShieldAction(base.Owner, base.Owner, this.Block, false);
            yield return new DamageAction(base.Owner, targets, this.Damage, base.GunName, GunType.Single);
            if (base.Battle.BattleShouldEnd)
            {
                yield break;
            }
            foreach (Unit unit in targets)
            {
                if (unit.IsAlive)
                {
                    yield return new ApplyStatusEffectAction<FirepowerNegative>(unit, base.Value1, null, null, null, 0f, true);
                }
            }
            Unit[] array = null;
            yield break;
        }
    }
}
