using Dominion.GameEvents;

namespace Dominion.AI
{
    public class Node
    {
        public virtual void Receive(NodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class Node<T> : Node
    {
        public virtual T Evaluate(TurnScope scope)
        {
            return default(T);
        }
    }
}
