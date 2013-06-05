using System.Collections.Generic;
using Dominion.Cards;

namespace Dominion
{
    public interface ISupplyBuilder
    {
        ISupplyBuilder With(IEnumerable<Card> cards);
        ISupplyBuilder WithPlayers(int playercount);
        ISupplyBuilder WithSet<T>() where T: DeckSet, new();
        ISupplyBuilder BasicGame();
        Supply BuildSupply();
    }
}