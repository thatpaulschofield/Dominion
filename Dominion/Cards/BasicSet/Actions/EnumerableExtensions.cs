using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Dominion.Cards.BasicSet.Actions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Append<T>(this IEnumerable list, T item) where T : class
        {
            return list.Cast<T>().Union<T>(new T[] { item as T });
        }
    }
}