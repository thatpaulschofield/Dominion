namespace Dominion.GameEvents
{
    public abstract class GameEventResponse : GameMessage, IEventResponse
    {
        protected GameEventResponse(IActionScope scope)
            : base(scope)
        {
        }

        public string Description { get; set; }

        public abstract void Execute();

        public override string ToString()
        {
            return Description;
        }
    }

    public abstract class GameEventResponse<TINRESPONSETO> : GameEventResponse
    {
        protected GameEventResponse(IActionScope scope) : base(scope)
        {
        }
    }


    public abstract class GameEventResponse<TINRESPONSETO, TITEM> : GameEventResponse<TINRESPONSETO>
    {
        protected GameEventResponse(IActionScope turnScope, TITEM item)
            : base(turnScope)
        {
            Item = item;
        }

        public TITEM Item { get; set; }
    }
}