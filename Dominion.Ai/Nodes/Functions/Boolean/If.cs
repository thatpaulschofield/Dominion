namespace Dominion.AI.Functions.Boolean
{
    public class If<T> : Function<T, bool, T, T>
    {
        public override T Evaluate(IAiContext context)
        {
            return (Child1.Evaluate(context)) ? Child2.Evaluate(context) : Child3.Evaluate(context);
        }

        public override string ToString()
        {
            return "if (" + typeof(T).Name + " value)";
        }
    }
}