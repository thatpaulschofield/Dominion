namespace Dominion.AI.Functions.Numeric
{
    public class Minus : Function<int, int, int>
    {
        public override int Evaluate(IAiContext context)
        {   
            return Child1.Evaluate(context) - Child2.Evaluate(context);
        }

        public override string ToString()
        {
            return "-";
        }
    }
}