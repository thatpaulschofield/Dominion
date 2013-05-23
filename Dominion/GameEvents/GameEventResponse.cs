﻿using Dominion.AI;

namespace Dominion.GameEvents
{
    public abstract class GameEventResponse
    {
        protected readonly TurnScope _turnScope;

        protected GameEventResponse(TurnScope turnScope)
        {
            _turnScope = turnScope;
        }

        public abstract void Execute();
    }
}