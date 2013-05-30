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
        Player ReceivingPlayer { get; }
        void RegisterEventFilter(ExternalEventFilter filter);
    }
}