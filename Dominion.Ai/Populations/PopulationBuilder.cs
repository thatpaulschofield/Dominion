using Dominion.AI;
using Dominion.Ai.Nodes;
using Dominion.Ai.TreeBuilding;

namespace Dominion.Ai.Populations
{
    public class PopulationBuilder<TROOTNODE, TTREEBUILDINGSTRATEGY>
        where TTREEBUILDINGSTRATEGY : ITreeBuildStrategy
        where TROOTNODE : INode, new()
    {
        private readonly TreeBuilder _treeBuilder;
        private readonly TTREEBUILDINGSTRATEGY _treebuildingstrategy;

        public PopulationBuilder(TreeBuilder treeBuilder,
                                 TTREEBUILDINGSTRATEGY treebuildingstrategy)
        {
            _treeBuilder = treeBuilder;
            _treebuildingstrategy = treebuildingstrategy;
        }

        public Population BuildPopulation(int size, TreeSpec spec)
        {
            var population = new Population();
            size.Times(() => population.Add(BuildAiStrategy(spec)));
            return population;
        }

        private AiStrategy BuildAiStrategy(TreeSpec spec)
        {
            var tree = _treeBuilder.BuildTree<TROOTNODE>(_treebuildingstrategy.WithSpec(spec));
            var aiStrategyId = new AiStrategy.AiStrategyId();
            return new AiStrategy(tree as Function<ResponseVotes>, aiStrategyId);
        }
    }
}