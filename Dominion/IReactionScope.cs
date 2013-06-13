using System;
using Dominion.Cards;
using Dominion.Cards.BasicSet.Actions;
using Dominion.GameEvents;

namespace Dominion
{
    public interface IReactionScope : IActionScope, IDisposable
    {
        ITurnScope OriginatingTurnScope { get; }
        Player OriginatingPlayer { get; }
        Player ReactingPlayer { get; }
        void RegisterEventFilter(ExternalEventFilter filter);
        void RevealCard(Card card);
        void PutCardFromHandOnTopOfDeck(Card card);
        void DrawCardsIntoHand(int count);
    }
}