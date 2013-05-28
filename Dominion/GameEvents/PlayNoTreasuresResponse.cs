using System;

namespace Dominion.GameEvents
{
    public class PlayNoTreasuresResponse : GameEventResponse
    {
        public PlayNoTreasuresResponse(ITurnScope turnScope) : base(turnScope)
        {
            Description = "Play no treasures";
        }

        public override void Execute()
        {
            // no-op
        }

        public override string ToString()
        {
            return String.Empty;
        }
    }
}