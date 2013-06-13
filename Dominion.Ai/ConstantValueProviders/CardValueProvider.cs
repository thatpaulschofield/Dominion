using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Cards;

namespace Dominion.Ai.ConstantValueProviders
{

    public class CardValueProvider :InitialValueProvider<Card>
    {
        private readonly CardTypeValueProvider _cardTypeValueProvider;

        public CardValueProvider(CardTypeValueProvider cardTypeValueProvider)
        {
            _cardTypeValueProvider = cardTypeValueProvider;
            ProvideValueInitializer = () => _cardTypeValueProvider.ProduceValue() as Card;
        }
    }

    public class CardTypeValueProvider : InitialValueProvider<CardType>
    {
        private readonly IList<CardType> _allCards;
        private readonly int _index;

        public CardTypeValueProvider(IEnumerable<CardType> allCards)
        {
            _allCards = allCards.ToList();
            _index = new Random((int)DateTime.Now.Ticks).Next(0, _allCards.Count() - 1);
        }

        public override Func<CardType> ProvideValueInitializer
        {
            get { return RandomCard; }
        }

        private CardType RandomCard()
        {
            return _allCards[_index];
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("CardTypeValueProvider:");
            _allCards.ForEach(c => sb.AppendLine("CardType: " + c.Create()));
            return sb.ToString();
        }
    }
}