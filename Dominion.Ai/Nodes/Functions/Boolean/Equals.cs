using System;

namespace Dominion.AI.Functions.Boolean
{


    public class LessThan<TValue> : Function<bool, TValue, TValue> where TValue : IComparable
    {
        public override bool Evaluate(IAiContext context)
        {
            return Child1.Evaluate(context).CompareTo(Child2.Evaluate(context)) < 0;
        }

        public override string ToString()
        {
            return "< (" + typeof(TValue).Name + " value)";
        }
    }

    public class GreaterThan<TValue> : Function<bool, TValue, TValue> where TValue : IComparable
    {
        public override bool Evaluate(IAiContext context)
        {
            return Child1.Evaluate(context).CompareTo(Child2.Evaluate(context)) > 0;
        }

        public override string ToString()
        {
            return "> (" + typeof(TValue).Name + " value)";
        }
    }

    public class Equals<TValue> : Function<bool, TValue, TValue>
    {
        public override bool Evaluate(IAiContext context)
        {
            return Child1.Evaluate(context).Equals(Child2.Evaluate(context));
        }

        public override string ToString()
        {
            return "== (" + typeof(TValue).Name + " value)";
        }
    }
}
