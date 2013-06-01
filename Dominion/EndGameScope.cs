﻿using System.Collections.Generic;
using Dominion.Cards;
using Dominion.GameEvents;

namespace Dominion
{
    internal class EndGameScope : ITurnScope
    {
        public Supply Supply { get; private set; }
        public int TurnNumber { get; private set; }
        public Player Player { get; private set; }
        public int PotentialCoins { get; private set; }
        public int Coins { get; private set; }
        public CardSet TreasuresInHand { get; private set; }
        public Hand Hand { get; private set; }
        public int Actions { get; private set; }
        public int Buys { get; private set; }
        public IEnumerable<Player> PassivePlayers { get; private set; }
        public IEnumerable<IReactionScope> ReactionScopes { get; private set; }

        public EndGameScope()
        {
            State = new StateStack();
        }

        public void Discard(CardSet cardsToDiscard)
        {
            
        }

        public void PerformBuy(CardType cardToPurchase)
        {
        }

        public void PlayTreasures(CardSet treasuresToPlay)
        {
        }

        public void Publish(IGameMessage @event)
        {
        }

        public StateStack State { get; private set; }
        public ITurnScope GetTurnScope { get; private set; }

        public void PlayAction(Card actionCard)
        {
        }

        public void PlayTreasure(Card treasureCard)
        {
        }

        public void ChangeState(TurnState delta)
        {
            
        }

        public void ChangeState(params TurnState[] deltas)
        {
            
        }

        public void CleanUp()
        {
        }

        public void TrashCard(Card card)
        {
            
        }

        public void GainCardFromSupply(CardType card)
        {
            
        }

        public T GetInstance<T>()
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            
        }
    }
}