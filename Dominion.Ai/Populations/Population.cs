using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominion.AI;

namespace Dominion.Ai.Populations
{
    public class Population : List<AiStrategy>
    {
        public Population()
        {
        }

        public Population(IEnumerable<AiStrategy> population)
        {
            this.AddRange(population);
        }
    }
}
