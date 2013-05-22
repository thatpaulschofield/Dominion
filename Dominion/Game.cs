using System;
using System.Collections.Generic;
using Dominion.Cards.BasicSet;
using Dominion.Cards.BasicSet.VictoryCards;

namespace Dominion
{
    public interface IHandleEvents<TEVENT> : IHandleEvents
    {
        void Handle(TEVENT @event);
    }

    public interface IHandleEvents
    {
        void Handle(object @event);
        bool CanHandle(object @event);
    }

    public interface IEventAggregator
    {
        void Register(IHandleEvents handler);
    }

    public class EventAggregator : IEventAggregator
    {
        private readonly List<IHandleEvents> _subscribers;

        public EventAggregator()
        {
            _subscribers = new List<IHandleEvents>();
        }

        public void Register(IHandleEvents handler)
        {
            _subscribers.Add(handler);
        }

        public void Publish(object @event)
        {
            _subscribers.ForEach(
                s =>
                    {
                        if (s.CanHandle(@event))
                            s.Handle(@event);
                    });
        }
    }

    public class Game
    {
        public static Deck DealStartupDeck()
        {
            return new Deck(7.Coppers(), 3.Estates()).Shuffle();
        }

        public static Game Initialize(int players)
        {
            int victoryCards = (players == 1 || players == 2) ? 12 : 8;
            int curseCards = (players - 1)*10;
            return new Game
                {
                    Supply = new Supply(new SupplyPile(victoryCards, Victory.Estate),
                        new SupplyPile(victoryCards, Victory.Duchy),
                        new SupplyPile(victoryCards, Victory.Province),
                        new SupplyPile(curseCards, BasicCards.Curse)
                        ),
                        Players = BuildPlayers(players)
                };
        }

        private static List<Player> BuildPlayers(int playerCount)
        {
            var players = new List<Player>();
            for (int i = 0; i < playerCount; i++)
            {
                players.Add(new Player(Game.DealStartupDeck(), new DiscardPile(), new NaivePlayerController()));
            }
            players.ForEach(p => p.DrawNewHand());
            return players;
        }

        public Supply Supply { get; private set; }

        public List<Player> Players { get; private set; }
    }
}
