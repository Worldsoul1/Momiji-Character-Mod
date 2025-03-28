﻿using Cysharp.Threading.Tasks;
//using DG.Tweening;
using LBoL.ConfigData;
using LBoL.Core.Units;
using LBoLEntitySideloader;
using LBoLEntitySideloader.Attributes;
using LBoLEntitySideloader.Entities;
using LBoLEntitySideloader.Resource;
using UnityEngine;
using Momiji.Source;
using Momiji.Source.ImageLoader;
using Momiji.Source.Localization;
using LBoL.Core.Battle;
using Momiji.Source.Ultimate;
//using SampleCharacterMod.BattleActions;

namespace Momiji
{
    public sealed class MomijiDef : PlayerUnitTemplate
    {
        public UniTask<Sprite>? LoadSpellPortraitAsync { get; private set; }

        public override IdContainer GetId()
        {
            return BepinexPlugin.modUniqueID;
        }

        public override LocalizationOption LoadLocalization()
        {
            return SampleCharacterLocalization.PlayerUnitBatchLoc.AddEntity(this);
        }

        public override PlayerImages LoadPlayerImages()
        {
            return SampleCharacterImageLoader.LoadPlayerImages(BepinexPlugin.playerName);
        }

        public override PlayerUnitConfig MakeConfig()
        {
            return SampleCharacterLoadouts.playerUnitConfig;
        }

        [EntityLogic(typeof(MomijiDef))]
        public sealed class Momiji : PlayerUnit
        {
            protected override void OnEnterBattle(BattleController battle)
            {
                base.OnEnterBattle(battle);
                if (this.Us is RabiesBite RabiesUltimateSkill)
                {
                    RabiesUltimateSkill.OnEnterBattle(battle);
                }

            }
        }
    }
}