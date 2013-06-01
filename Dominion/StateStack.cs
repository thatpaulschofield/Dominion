using System.Collections.Generic;
using Dominion.Cards.BasicSet.Actions.MineAction;

namespace Dominion
{
    public class StateStack : Stack<CardState>
    {
        public CardState<T> Pop<T>()
        {
            return Pop() as CardState<T>;
        }

        public CardState<T> Peek<T>()
        {
            return Peek() as CardState<T>;
        }
    }
}