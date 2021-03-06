﻿using System;
using System.Collections.Generic;
using Dominion.Cards;
using Dominion.GameEvents;

namespace Dominion
{
    public interface ITurnScope : IActionScope, IDisposable
    {
        CardSet TreasuresInHand { get; }
        int TurnNumber { get; }
        int Coins { get; }
        int Actions { get; }
        int Buys { get; }
        IEnumerable<IReactionScope> ReactionScopes { get; }
        CardSet CardsInPlay { get; }
        CardSet Deck { get; }
        CardSet DiscardPile { get; }
        void Discard(CardSet cardsToDiscard);
        void PlayTreasures(CardSet treasuresToPlay);
        void Publish(IGameMessage @event);
        void PlayAction(Card actionCard);
        void PlayTreasure(Card treasureCard);
        void ChangeState(TurnState delta);
        void ChangeState(params TurnState[] deltas);
        void CleanUp();
        string ToString();
        void TrashCardFromHand(Card card);
        void GainCardFromSupply(CardType card);
        T GetInstance<T>();
        CardSet FindCardsEligibleForPurchase(ITurnScope turnScope);
        void PutCardFromHandIntoPlay(Card card);
        void GainCardFromSupplyOntoTopOfDeck(Card card);
        void RevealCard(Card card);
        void PutCardOnTopOfDeck(Card card);
        void PutCardFromHandOnTopOfDeck(Card card);
        Card DrawCard();
        Card RevealCardFromTopOfDeck();
        void PutCardsIntoHand(CardSet cards);
        void PutCardsIntoDiscardPile(CardSet cardSet);
        void DrawCardsIntoHand(int count);
        void DrawCardIntoCardset(CardSet cardSet);
        CardSet MoveCardsFrom (CardSet currentLocation);
        void TrashCardInPlay(CardType cardType);
    }
}