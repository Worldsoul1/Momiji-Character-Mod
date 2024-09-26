using LBoLEntitySideloader.Attributes;
using LBoL.Core.StatusEffects;
using Momiji.Source.StatusEffects;

namespace Momiji.Source.StatusEffects
{
    public sealed class SampleCharacterCustomKeywordSeDef : SampleCharacterStatusEffectTemplate
    {

    }

    [EntityLogic(typeof(SampleCharacterCustomKeywordSeDef))]
    public sealed class SampleCharacterCustomKeywordSe : StatusEffect
    {
    }
}
