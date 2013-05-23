using System;
using System.Linq;
using Dominion.Cards;

namespace Dominion
{
    public class TurnScope
    {
        private readonly CardSet _cardsToDiscard = new CardSet();
        private readonly Player _player;
        private readonly DiscardPile _discardPile;
        private int _remainingBuys;
        private readonly CardSet _cardsInPlay = new CardSet();

        public TurnScope(Player player, Supply supply, DiscardPile discardPile)
        {
            _remainingBuys = 1;

            if (discardPile == null)
                throw new ArgumentNullException("DiscardPile cannot be null");
            if (supply == null)
                throw new ArgumentNullException("Supply cannot be null.");

            _player = player;
            _discardPile = discardPile;
            Supply = supply;
            Coins = 0;
        }

        

        public void Discard(CardSet cardsToDiscard)
        {
            _cardsToDiscard.AddRange(cardsToDiscard);
        }

        public Supply Supply { get; private set; }

        public int Coins { get; private set; }

        public Player Player
        {
            get { return _player; }
        }

        public int PotentialCoins
        {
            get { return Coins + this.Player.Hand.Treasures().Sum(c => c.Coins); }
        }

        public void CleanUp()
        {
            _cardsToDiscard.DiscardInto(_discardPile);
        }

        public void PerformBuy(CardType cardToPurchase, CardSet treasuresToPlay)
        {
            if (_remainingBuys <= 0)
                throw new OutOfBuysException();

            PlayTreasures(treasuresToPlay);
            this.Discard(Supply.AcquireCard(cardToPurchase));
            _remainingBuys--;
        }

        public void PlayTreasures(CardSet treasuresToPlay)
        {
            _player.PlayTreasures(treasuresToPlay, _cardsInPlay);

        }
    }

    public class OutOfBuysException : Exception
    {
    }
}