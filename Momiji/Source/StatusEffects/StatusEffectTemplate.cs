using LBoL.ConfigData;
using LBoLEntitySideloader;
using LBoLEntitySideloader.Entities;
using LBoLEntitySideloader.Resource;
using UnityEngine;
using Momiji.Source.ImageLoader;
using Momiji.Source.Localization;

namespace Momiji.Source.StatusEffects
{
    public class SampleCharacterStatusEffectTemplate : StatusEffectTemplate
    {
        public override IdContainer GetId()
        {
            return SampleCharacterDefaultConfig.GetDefaultID(this);
        }

        public override LocalizationOption LoadLocalization()
        {
            return SampleCharacterLocalization.StatusEffectsBatchLoc.AddEntity(this);
        }

        public override Sprite LoadSprite()
        {
            return SampleCharacterImageLoader.LoadStatusEffectLoader(status: this);
        }

        public override StatusEffectConfig MakeConfig()
        {
            return GetDefaultStatusEffectConfig();
        }

        public static StatusEffectConfig GetDefaultStatusEffectConfig(EntityDefinition entity = null)
        {
            return SampleCharacterDefaultConfig.GetDefaultStatusEffectConfig(entity);
        }   
        
    }
}