using System.Collections.Generic;
using Dominion.Cards;

namespace Dominion.Ai.Nodes.Functions.Numeric
{
    public interface IWantToViewAllCardTypes
    {
        void ShowAllCardTypes(IEnumerable<CardType> cardTypes);
    }
}