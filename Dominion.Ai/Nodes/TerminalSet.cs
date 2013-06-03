using System.Collections.Generic;
using System.Linq;
using Dominion.AI;
using Dominion.Ai.Nodes;
using StructureMap;

namespace Dominion.Ai
{
    public class TerminalSet
    {
        private readonly IContainer _container;

        public TerminalSet(IContainer container)
        {
            _container = container;
        }
        public IEnumerable<INode> TerminalNodes()
        {
            var functions = _container.GetAllInstances(typeof (Function));
                
                return functions.Cast<INode>();
        }
    }
}