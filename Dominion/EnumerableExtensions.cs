using System;
using System.Collections.Generic;
using System.Linq;

namespace Dominion
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            items.ToList().ForEach(action);
        }
    }
}