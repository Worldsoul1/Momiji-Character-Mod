using LBoL.ConfigData;
using LBoLEntitySideloader;
using LBoLEntitySideloader.Entities;
using LBoLEntitySideloader.Resource;
using UnityEngine;
using Momiji.Source.ImageLoader;
using Momiji.Source.Localization;

namespace Momiji.Source.Ultimate
{
    public class SampleCharacterUltTemplate : UltimateSkillTemplate
    {
        public override IdContainer GetId()
        {
            return SampleCharacterDefaultConfig.GetDefaultID(this);
        }

        public override LocalizationOption LoadLocalization()
        {
            return SampleCharacterLocalization.UltimateSkillsBatchLoc.AddEntity(this);
        }

        public override Sprite LoadSprite()
        {
            return SampleCharacterImageLoader.LoadUltLoader(ult: this);
        }

        public override UltimateSkillConfig MakeConfig()
        {
            throw new System.NotImplementedException();
        }

        public UltimateSkillConfig GetDefaulUltConfig()
        {
            return SampleCharacterDefaultConfig.GetDefaultUltConfig();
        }
    }
}