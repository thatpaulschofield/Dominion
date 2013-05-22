namespace Dominion.AI.Functions
{
    public class If<TRETURN> : Function<TRETURN, bool, TRETURN, TRETURN>
    {
        public override TRETURN Evaluate(TurnScope scope)
        {
            return Child1.Evaluate(scope) ? Child2.Evaluate(scope) : Child3.Evaluate(scope);
        }
    }
}
