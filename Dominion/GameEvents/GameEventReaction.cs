﻿using System.Linq;

namespace Dominion.GameEvents
{
    public abstract class GameEventReaction : GameMessage, IEventReaction
    {
        protected readonly ITurnScope TurnScope;

        protected GameEventReaction(IReactionScope scope)
            : base(scope)
        {
            ReactionScope = scope;
        }

        protected IReactionScope ReactionScope { get; set; }

        public string Description { get; set; }

        public abstract void Execute();

        public override string ToString()
        {
            return Description;
        }
    }

    public abstract class GameEventReaction<TINRESPONSETO> : GameEventReaction
    {
        protected GameEventReaction(IReactionScope scope) : base(scope)
        {
        }
    }


    public abstract class GameEventReaction<TINRESPONSETO, TITEM> : GameEventReaction<TINRESPONSETO>
    {
        protected GameEventReaction(IReactionScope scope, TITEM item)
            : base(scope)
        {
            Item = item;
        }

        public TITEM Item { get; protected set; }

    }
}