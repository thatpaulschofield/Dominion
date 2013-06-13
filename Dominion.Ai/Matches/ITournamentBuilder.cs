using Dominion.Ai.Populations;

namespace Dominion.Ai.Matches
{
    public interface ITournamentBuilder
    {
        Tournament Build(int seriesSize, int gameSize, Population population);
    }
}