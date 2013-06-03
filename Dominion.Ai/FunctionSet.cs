using System.Collections.Generic;
using System.Linq;
using Dominion.AI;
using Dominion.Ai.Nodes;
using StructureMap;

namespace Dominion.Ai
{
    public class FunctionSet
    {
        private readonly IContainer _container;

        public FunctionSet(IContainer container)
        {
            _container = container;
        }

        public IEnumerable<INode> FunctionNodes()
        {
            return _container.GetAllInstances(typeof (Function)).Cast<INode>();
        }
    }
}