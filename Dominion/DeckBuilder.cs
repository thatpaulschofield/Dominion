using System.Collections.Generic;
using Dominion.Cards;

namespace Dominion
{
    public class DeckBuilder
    {
        private readonly List<CardSet> _cardSets = new List<CardSet>();

        public DeckBuilder WithSets(params CardSet[] cardSets)
        {
            _cardSets.AddRange(cardSets);
            return this;
        }

        public static implicit operator Deck(DeckBuilder builder)
        {
            return builder.Build();
        }

        public Deck Build()
        {
            return new Deck(_cardSets.ToArray());
        }
    }
}