using System;
using Dominion.AI;
using Dominion.Ai.Nodes.Functions;
using Dominion.Ai.TreeBuilding;
using StructureMap;

namespace Dominion.Ai
{
    public class AiPlayerBuilder
    {
        private readonly IContainer _container;
        private TreeSpec _treeSpec = new TreeSpec {MaxDepth = 25};

        public AiPlayerBuilder(IContainer container)
        {
            _container = container;
        }

        public AiPlayerBuilder WithTreeSpec(TreeSpec treeSpec)
        {
            _treeSpec = treeSpec;
            return this;
        }

        public PlayerSpec BuildAiPlayer(string playerName)
        {
            return BuildAiPlayer(playerName, Guid.NewGuid());
        }

        public PlayerSpec BuildAiPlayer(string playerName, Guid playerId)
        {
            var childContainer = _container.GetNestedContainer();
            var treeBuilder = childContainer.GetInstance<TreeBuilder>();
            var fullTree = childContainer.GetInstance<FullTreeStrategy>();
            var aiTree =
                treeBuilder.BuildTree<CombineVotes>(fullTree.WithSpec(_treeSpec));
            var aiController = new AiPlayerController(new AiStrategy(aiTree as Function<ResponseVotes>));
            var aiPlayer = new PlayerSpec().WithPlayerName(playerName).WithController(aiController).WithId(playerId);
            return aiPlayer;
            
        }
    }
}