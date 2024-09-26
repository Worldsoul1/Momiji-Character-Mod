using System.Collections.Generic;
using System.Linq;
using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoL.EntityLib.Cards.Neutral.NoColor;
using LBoL.EntityLib.Exhibits;
using LBoLEntitySideloader.Attributes;
using Momiji.Source.Exhibits;

namespace Momiji.Source.Exhibits
{
    public sealed class HeavyBladeDef : SampleCharacterExhibitTemplate
    {
        public override ExhibitConfig MakeConfig()
        {
            ExhibitConfig exhibitConfig = this.GetDefaultExhibitConfig();

            exhibitConfig.Value1 = 2;
            exhibitConfig.Mana = new ManaGroup() { Red = 1 };
            exhibitConfig.BaseManaColor = ManaColor.Red;
            exhibitConfig.RelativeCards = new List<string>() { nameof(PManaCard) };

            return exhibitConfig;
        }
    }

    [EntityLogic(typeof(HeavyBladeDef))]
    public sealed class HeavyBlade : ShiningExhibit
    {
        private readonly HashSet<Unit> TriggeredEnemies = new HashSet<Unit>();

        protected override void OnEnterBattle()
        {
            TriggeredEnemies.Clear();

            base.ReactBattleEvent<StatisticalDamageEventArgs>(base.Battle.Player.StatisticalTotalDamageDealt, this.OnStatisticalDamageDealt);
        }

        private IEnumerable<BattleAction> OnStatisticalDamageDealt(StatisticalDamageEventArgs args)
        {
            if (base.Battle.BattleShouldEnd)
            {
                yield break;
            }

            bool activated = false;

            foreach ((Unit unit, IReadOnlyList<DamageEventArgs> dmg) in args.ArgsTable)
            {
                if (unit.IsAlive)
                {
                    if (dmg.Count(dmgArgs =>
                    {
                        DamageInfo damageInfo = dmgArgs.DamageInfo;
                        return damageInfo.DamageType == DamageType.Attack && damageInfo.Amount > 0f;
                    }) > 0)
                    {
                        if (!TriggeredEnemies.Contains(unit))
                        {
                            if (!activated)
                            {
                                base.NotifyActivating();
                                activated = true;
                            }
                            TriggeredEnemies.Add(unit);
                            yield return new ApplyStatusEffectAction<TempFirepowerNegative>(unit, base.Value1, null, null, null, 0f, true);
                        }

                    }
                }
            }
            yield break;
        }
    }
}