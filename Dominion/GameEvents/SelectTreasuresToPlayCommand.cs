using System;

namespace Dominion.GameEvents
{
    public class SelectTreasuresToPlayCommand : GameCommand
    {
        public SelectTreasuresToPlayCommand(ITurnScope turnScope) : base(turnScope)
        {
            Description = String.Format("{0}, select treasures to play", turnScope.Player.Name);
            _availableResponses.Add(new PlayAllTreasuresResponse(turnScope));
            _availableResponses.Add(new PlayNoTreasuresResponse(turnScope));
        }

        public override string ToString()
        {
            return TurnScope.TreasuresInHand.ToString();
        }
    }
}