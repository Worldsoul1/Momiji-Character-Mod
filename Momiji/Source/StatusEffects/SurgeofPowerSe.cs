using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoLEntitySideloader.Attributes;

namespace Momiji.Source.StatusEffects
{
    public sealed class SurgeofPowerSeDef : SampleCharacterStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();
            config.HasLevel = true;
            return config;
        }
    }
    [EntityLogic(typeof(SurgeofPowerSeDef))]
    public sealed class SurgeofPowerSe : StatusEffect
    {
        // Token: 0x0600007A RID: 122 RVA: 0x00002D34 File Offset: 0x00000F34
        protected override void OnAdded(Unit unit)
        {
            this.CardToFree(base.Battle.EnumerateAllCards());
            base.ReactOwnerEvent<CardUsingEventArgs>(base.Battle.CardUsed, new EventSequencedReactor<CardUsingEventArgs>(this.OnCardUsed));
            base.HandleOwnerEvent<CardsEventArgs>(base.Battle.CardsAddedToDiscard, new GameEventHandler<CardsEventArgs>(this.OnAddCard));
            base.HandleOwnerEvent<CardsEventArgs>(base.Battle.CardsAddedToHand, new GameEventHandler<CardsEventArgs>(this.OnAddCard));
            base.HandleOwnerEvent<CardsEventArgs>(base.Battle.CardsAddedToExile, new GameEventHandler<CardsEventArgs>(this.OnAddCard));
            base.HandleOwnerEvent<CardsAddingToDrawZoneEventArgs>(base.Battle.CardsAddedToDrawZone, new GameEventHandler<CardsAddingToDrawZoneEventArgs>(this.OnAddCardToDraw));
        }

        // Token: 0x0600007B RID: 123 RVA: 0x00002DE3 File Offset: 0x00000FE3
        private void OnAddCardToDraw(CardsAddingToDrawZoneEventArgs args)
        {
            this.CardToFree(args.Cards);
        }

        // Token: 0x0600007C RID: 124 RVA: 0x00002DF1 File Offset: 0x00000FF1
        private void OnAddCard(CardsEventArgs args)
        {
            this.CardToFree(args.Cards);
        }

        // Token: 0x0600007D RID: 125 RVA: 0x00002E00 File Offset: 0x00001000
        private void CardToFree(IEnumerable<Card> cards)
        {
            foreach (Card card in cards)
            {
                if (card.CardType == CardType.Attack || card.CardType == CardType.Skill || card.CardType == CardType.Defense || card.CardType == CardType.Ability)
                {
                    card.FreeCost = true;
                }
            }
        }

        // Token: 0x0600007E RID: 126 RVA: 0x00002E54 File Offset: 0x00001054
        protected override void OnRemoved(Unit unit)
        {
            foreach (Card card in base.Battle.EnumerateAllCards())
            {
                if (card.CardType == CardType.Attack || card.CardType == CardType.Skill || card.CardType == CardType.Defense || card.CardType == CardType.Ability)
                {
                    card.FreeCost = false;
                }
            }
        }

        // Token: 0x0600007F RID: 127 RVA: 0x00002EB0 File Offset: 0x000010B0
        private IEnumerable<BattleAction> OnCardUsed(CardUsingEventArgs args)
        {
            if (args.Card.CardType == CardType.Attack || args.Card.CardType == CardType.Skill || args.Card.CardType == CardType.Defense || args.Card.CardType == CardType.Ability)
            {
                int level = base.Level - 1;
                base.Level = level;
                if (base.Level <= 0)
                {
                    yield return new RemoveStatusEffectAction(this, true, 0.1f);
                }
            }
            yield break;
        }

        // Token: 0x04000003 RID: 3
        [UsedImplicitly]
        public ManaGroup Mana = ManaGroup.Empty;
    }
}
