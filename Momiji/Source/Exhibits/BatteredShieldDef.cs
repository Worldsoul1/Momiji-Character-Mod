using System.Collections.Generic;
using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.StatusEffects;
using LBoL.EntityLib.Exhibits;
using LBoLEntitySideloader.Attributes;
using Momiji.Source.Exhibits;
using LBoL.EntityLib.StatusEffects.Basic;
using LBoL.Core.Battle.BattleActions;

namespace Momiji.Source.Exhibits
{
    public sealed class BatteredShieldDef : SampleCharacterExhibitTemplate
    {   

        public override ExhibitConfig MakeConfig()
        {

            ExhibitConfig exhibitConfig = this.GetDefaultExhibitConfig();
            exhibitConfig.Value1 = 3;
            exhibitConfig.Mana = new ManaGroup() { White = 1 };
            exhibitConfig.BaseManaColor = ManaColor.White;

            exhibitConfig.RelativeEffects = new List<string>() { nameof(Reflect) };
            
            return exhibitConfig;
        }
    }

    [EntityLogic(typeof(BatteredShieldDef))]
    public sealed class BatteredShield : ShiningExhibit
    {
        private bool Triggered = false;

        protected override void OnEnterBattle()
        {
            base.HandleBattleEvent<UnitEventArgs>(base.Owner.TurnStarting, new GameEventHandler<UnitEventArgs>(this.OnTurnStarting));

            base.ReactBattleEvent<BlockShieldEventArgs>(base.Owner.BlockShieldGained, new EventSequencedReactor<BlockShieldEventArgs>(this.OnBlockShieldGained));
        }

        protected override void OnLeaveBattle()
        {
            Triggered = false;
        }

        private void OnTurnStarting(UnitEventArgs args)
        {
            Triggered = false;
        }

        private IEnumerable<BattleAction> OnBlockShieldGained(BlockShieldEventArgs args)
        {
            if (args.Block > 0f || args.Shield > 0f)
            {
                if (!Triggered)
                {
                    Triggered = true;
                    base.NotifyActivating();
                    yield return new ApplyStatusEffectAction<Reflect>(base.Owner, base.Value1, null, null, null, 0.1f, true);
                    if (base.Battle.Player.HasStatusEffect<Reflect>())
                    {
                        base.Battle.Player.GetStatusEffect<Reflect>().Gun = ("心抄斩");
                    }
                }
            }

            yield break;
        }
    }
}