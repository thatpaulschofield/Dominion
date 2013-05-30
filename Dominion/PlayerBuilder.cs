namespace Dominion
{
    public class PlayerBuilder
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly DeckBuilder _deckBuilder;
        private IPlayerController _controller;
        private string _playerName;

        public PlayerBuilder(IEventAggregator eventAggregator, DeckBuilder deckBuilder)
        {
            _eventAggregator = eventAggregator;
            _deckBuilder = deckBuilder;
        }

        public PlayerBuilder WithController(IPlayerController controller)
        {
            _controller = controller;
            return this;
        }

        public static implicit operator Player(PlayerBuilder builder)
        {
            return builder.Build();
        }

        private Player Build()
        {
            return new Player(_deckBuilder, new DiscardPile(), _controller, name: _playerName);
        }

        public PlayerBuilder WithName(string playerName)
        {
            _playerName = playerName;
            return this;
        }
    }
}