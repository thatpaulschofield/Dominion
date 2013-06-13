namespace Dominion.AI.Functions.Boolean
{
    public class ConditionalFunction : Function<ResponseVotes, bool, ResponseVotes>
    {
        public override ResponseVotes Evaluate(IAiContext context)
        {
            return Child1.Evaluate(context) ? Child2.Evaluate(context) : new ResponseVotes();
        }

        public override string ToString()
        {
            return "if {1} then {2}";
        }
    }

    public class ConditionalWithElseFunction : Function<ResponseVotes, bool, ResponseVotes, ResponseVotes>
    {
        public override ResponseVotes Evaluate(IAiContext context)
        {
            return Child1.Evaluate(context) ? Child2.Evaluate(context) : Child3.Evaluate(context);
        }

        public override string ToString()
        {
            return "if {1} then {2} else {3}";
        }
    }



    public class And : Function<bool, bool, bool>
    {
        public override bool Evaluate(IAiContext context)
        {
            return Child1.Evaluate(context) && Child2.Evaluate(context);
        }

        public override string ToString()
        {
            return "and";
        }
    }
}