using System;
using System.Collections.Generic;
using Dominion.AI;

namespace Dominion.Ai
{
    public class TypeSet
    {
        public IEnumerable<Type> Types()
        {
            yield return typeof (int);
            yield return typeof (bool);
            yield return typeof (ResponseVotes);
        }
    }
}