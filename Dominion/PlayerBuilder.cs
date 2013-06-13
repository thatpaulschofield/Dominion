using System;

namespace Dominion
{
    public class PlayerBuilder
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly DeckBuilder _deckBuilder;
        private IPlayerController _controller;
        private string _playerName;
        private PlayerSpec _spec;
        private Guid _id;

        public PlayerBuilder(IEventAggregator eventAggregator, DeckBuilder deckBuilder)
        {
            _eventAggregator = eventAggregator;
            _deckBuilder = deckBuilder;
        }

        public static implicit operator Player(PlayerBuilder builder)
        {
            return builder.Build();
        }

        private Player Build()
        {
            return new Player(_deckBuilder.Build(), new DiscardPile(), _controller, name: _playerName, id: _id);
        }

        public PlayerBuilder WithName(string playerName)
        {
            _playerName = playerName;
            return this;
        }

        public PlayerBuilder ForSpec(PlayerSpec playerSpec)
        {
            if (playerSpec.PlayerId == null)
                throw new ArgumentNullException("playerSpec.PlayerId");
            _spec = playerSpec;
            _playerName = playerSpec.PlayerName;
            _controller = playerSpec.Controller;
            _id = playerSpec.PlayerId;
            return this;
        }
    }
}