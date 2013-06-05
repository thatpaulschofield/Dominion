namespace Dominion.GameEvents
{
    public class PlayAllTreasuresResponse : GameEventResponse<SelectTreasuresToPlayCommand>
    {
        public PlayAllTreasuresResponse(ITurnScope turnScope) : base(turnScope)
        {
            Description = "Play all treasures";
        }

        public override void Execute()
        {
            TurnScope.PlayTreasures(TurnScope.TreasuresInHand);
        }

        public override string ToString()
        {
            return Description;
        }
    }
}