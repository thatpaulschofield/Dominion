using Dominion.AI.Functions.Boolean;

namespace Dominion.Cards.BasicSet.Actions
{
    public class Market : TypedCard<Market>
    {
        public Market()
            : base(isAction:true, cost: 5, name: "Market")
        {

        }

        public override void PlayAsAction(ITurnScope turnScope)
        {
            turnScope.ChangeState(new TurnState(1, 1, 1));
            turnScope.Player.Draw(1, turnScope);
        }
    }

    public class Militia : TypedCard<Militia>
    {
        public Militia()
            : base(isAction: true, cost: 5, name: "Militia")
        {
        }
    }

    public class Mine : TypedCard<Mine>
    {
        public Mine()
            : base(isAction: true, cost: 5, name: "Mine")
        {
        }
    }

    public class Moat : TypedCard<Moat>
    {
        public Moat()
            : base(isAction: true, isAttack: true, cost: 2, name: "Moat")
        {
        }

        public override void PlayAsAction(ITurnScope turnScope)
        {
            turnScope.Player.Draw(2, turnScope);
        }
    }

    public class Remodel : TypedCard<Remodel>
    {
        public Remodel()
            : base(isAction: true, cost: 4, name: "Remodel")
        {
        }
    }

    public class Smithy : TypedCard<Smithy>
    {
        public Smithy()
            : base(isAction: true, cost: 3, name: "Smithy")
        {
        }

        public override void PlayAsAction(ITurnScope turnScope)
        {
            turnScope.Player.Draw(3, turnScope);
        }
    }

    public class Village : TypedCard<Village>
    {
        public Village()
            : base(isAction: true, cost: 3, name: "Village")
        {
        }

        public override void PlayAsAction(ITurnScope turnScope)
        {
            turnScope.ChangeState(1.TurnActions());
            turnScope.Player.Draw(2, turnScope);
        }
    }

    public class Woodcutter : TypedCard<Woodcutter>
    {
        public Woodcutter()
            : base(isAction: true, cost: 3, name: "Woodcutter")
        {
        }

        public override void PlayAsAction(ITurnScope turnScope)
        {
            turnScope.ChangeState(1.TurnBuys(), 2.TurnCoins());
        }
    }


    public class Workshop : TypedCard<Workshop>
    {
        public Workshop()
            : base(isAction: true, cost: 3, name: "Workshop")
        {

        }
    }
}

