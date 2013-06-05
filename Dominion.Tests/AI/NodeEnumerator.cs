using System.Collections.Generic;
using Dominion.Ai.Nodes;

namespace Dominion.Tests.AI
{
    public class NodeEnumerator : NodeVisitor
    {
        readonly List<INode> _nodes = new List<INode>();
        public void Visit(INode node)
        {
            _nodes.Add(node);
        }

        public IEnumerable<INode> Nodes { get { return _nodes; } }
    }
}