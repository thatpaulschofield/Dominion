using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.Cards;
using Dominion.GameEvents;

namespace Dominion
{
    public class DeckBuilder
    {
        private readonly IEventAggregator _eventAggregator;
        private List<CardSet> _cardSets = new List<CardSet>();

        public DeckBuilder(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public DeckBuilder WithSets(params CardSet[] cardSets)
        {
            _cardSets.AddRange(cardSets);
            return this;
        }

        public static implicit operator Deck(DeckBuilder builder)
        {
            return builder.Build();
        }

        private Deck Build()
        {
            return new Deck(_eventAggregator, _cardSets.ToArray());
        }
    }

    public class Deck : CardSet
    {
        private readonly IEventAggregator _eventAggregator;

        public Deck(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public Deck(params CardSet[] cardSets)
        {
            cardSets.ToList().ForEach(_innerList.AddRange);
        }

        public Deck(IEventAggregator eventAggregator, params CardSet[] cardSets)
        {
            _eventAggregator = eventAggregator;
            cardSets.ToList().ForEach(_innerList.AddRange);
        }

        public bool CanDraw
        {
            get { return this.ToList().Any(); }
        }

        public Card Draw(ITurnScope turnScope)
        {
            if (!_innerList.Any())
            {
                turnScope.Player.Handle(new DeckDepletedEvent(turnScope));
            }
            if (_innerList.Any())
            {
                var drawn = _innerList[0];
                _innerList.RemoveAt(0);
                return drawn;
            }
            return null;
        }

        public Deck Shuffle()
        {
            if (!_innerList.Any())
                return this;

            var shuffled = this;
            for (int i = 0; i < 100; i++)
            {
                int posA = new Random((int)DateTime.Now.Ticks).Next(shuffled._innerList.Count);
                int posB = new Random((int)DateTime.Now.Ticks).Next(shuffled._innerList.Count);
                Card temp = shuffled._innerList[posA];
                shuffled._innerList[posA] = shuffled._innerList[posB];
                shuffled._innerList[posB] = temp;
            }
            return shuffled;
        }

        public CardSet Draw(int cardCount, ITurnScope turnScope)
        {
            var cardSet = new CardSet();
            cardCount.Times(() => cardSet.Add(Draw(turnScope), turnScope));
            return cardSet;
        }
    }
}