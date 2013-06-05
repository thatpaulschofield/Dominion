using System;
using System.Collections.Generic;

namespace Dominion.Ai.Nodes.Functions
{
    public class SourceEventGroup
    {
        public Type Source { get; set; }

        public List<Type> Responses { get; set; }
    }
}