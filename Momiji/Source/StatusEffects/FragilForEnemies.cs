using System.Collections.Generic;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoLEntitySideloader.Attributes;
using System;
using UnityEngine;

namespace Momiji.Source.StatusEffects
{
    // Token: 0x02000095 RID: 149
    public sealed class FragilForEnemiesDef : SampleCharacterStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            config.HasLevel = false;
            config.HasDuration = true;
            return config;
        }
    }

    // Token: 0x17000245 RID: 581
    // (get) Token: 0x06000706 RID: 1798 RVA: 0x00014770 File Offset: 0x00012970
    [EntityLogic(typeof(FragilForEnemiesDef))]
    public sealed class FragilForEnemies : StatusEffect
    { 
        public int Value
        {
            get
            {
                GameRunController gameRun = base.GameRun;
                if (gameRun == null || !(base.Owner is EnemyUnit))
                {
                    return 30;
                }
                return Math.Min(30 + gameRun.FragilExtraPercentage, 100);
            }
        }

        // Token: 0x06000707 RID: 1799 RVA: 0x000147A8 File Offset: 0x000129A8
        protected override void OnAdded(Unit unit)
        {
            if (unit is PlayerUnit)
            {
                base.HandleOwnerEvent<BlockShieldEventArgs>(unit.BlockShieldGaining, new GameEventHandler<BlockShieldEventArgs>(this.OnBlockGaining));
                return;
            }
            Debug.LogError(this.Name + " added to enemy " + unit.Name + ", which has no effect.");
        }

        // Token: 0x06000708 RID: 1800 RVA: 0x000147F8 File Offset: 0x000129F8
        private void OnBlockGaining(BlockShieldEventArgs args)
        {
            float num = 1f - (float)this.Value / 100f;
            if (args.Type == BlockShieldType.Direct)
            {
                return;
            }
            args.Block *= num;
            args.Shield *= num;
        }
    }
}

