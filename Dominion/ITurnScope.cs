using System;
using System.Collections.Generic;
using Dominion.Cards;
using Dominion.GameEvents;

namespace Dominion
{
    public interface ITurnScope : IActionScope, IDisposable
    {
        Supply Supply { get; }
        CardSet TreasuresInHand { get; }
        int TurnNumber { get; }
        int Coins { get; }
        int Actions { get; }
        int Buys { get; }
        IEnumerable<IReactionScope> ReactionScopes { get; }
        CardSet CardsInPlay { get; }
        void Discard(CardSet cardsToDiscard);
        void PlayTreasures(CardSet treasuresToPlay);
        void Publish(IGameMessage @event);
        void PlayAction(Card actionCard);
        void PlayTreasure(Card treasureCard);
        void ChangeState(TurnState delta);
        void ChangeState(params TurnState[] deltas);
        void CleanUp();
        string ToString();
        void TrashCard(Card card);
        void GainCardFromSupply(CardType card);
        T GetInstance<T>();
        CardSet FindCardsEligibleForPurchase(ITurnScope turnScope);
    }
}