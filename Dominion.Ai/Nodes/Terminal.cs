using Dominion.AI;

namespace Dominion.Ai.Nodes
{
    public abstract class Terminal<T> : Node<T>, Terminal 
    {

        public ITurnScope TurnScope { get; set; }

        public IAiContext AiContext { get; set; }

    }

    public interface Terminal : INode
    {
    }
}