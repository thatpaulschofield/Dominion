using System;
using Dominion.Cards.BasicSet.Actions.Saga;
using Dominion.Cards.Saga;
using Stateless;

namespace Dominion.Cards.BasicSet.Actions.MineAction
{
    public class MineSaga : Saga<MineSaga.State, MineSaga.Trigger>, 
        IStartedBy<Mine.MinePlayedMessage>,
        IRespondTo<CardSelectedToTrashForMineEvent>,
        IRespondTo<DeclinedToTrashCardForMineResponse>
    {
        private Card _cardToTrash;

        public MineSaga() : base(State.Initial)
        {
            Configure(State.Initial)
                .PermitDynamic(Event<Mine.MinePlayedMessage>(), m => State.MinePlayed);
            Configure(State.MinePlayed)
                .OnEntryFrom(Event<Mine.MinePlayedMessage>(), RequestSelectionOfTreasureToTrash)
                .PermitDynamic(Event<CardSelectedToTrashForMineEvent>(), m => State.CardSelectedToTrash)
                .PermitDynamic(Event<DeclinedToTrashCardForMineResponse>(), m => State.Complete);
            Configure(State.CardSelectedToTrash)
                .OnEntryFrom(Event<CardSelectedToTrashForMineEvent>(), SelectCardToUpgradeTo)
                .PermitDynamic(Event<TreasurePickedToUpgradeToWithMine>(), m => State.Complete);
            Configure(State.Complete)
                .OnEntryFrom(Event<TreasurePickedToUpgradeToWithMine>(), PerformUpgrade);
            CompletedState = State.Complete;
        }

        private void RequestSelectionOfTreasureToTrash(Mine.MinePlayedMessage message)
        {
            Id = message.CorrelationId;
            message.TurnScope.Publish(new PickTreasureToTrashForMineCommand(message.TurnScope) { Id = Id, CorrelationId = Id, OriginalEventId = Id });
        }

        private void SelectCardToUpgradeTo(CardSelectedToTrashForMineEvent @event)
        {
            _cardToTrash = @event.CardToTrash;
            @event.TurnScope.Publish(new PickTreasureToUpgradeToForMineCommand(@event.CardToTrash.BaseCost + 3, @event.TurnScope){ Id = Guid.NewGuid(), CorrelationId = @event.OriginalEventId, OriginalEventId = @event.OriginalEventId});            
        }

        private void PerformUpgrade(TreasurePickedToUpgradeToWithMine @event)
        {
            //@event.TurnScope.Publish(new TrashCardFromHandCommand(_cardToTrash, @event.TurnScope));
            @event.TurnScope.Player.TrashCardFromHand(_cardToTrash, @event.TurnScope);
            @event.TurnScope.Player.GainCardFromSupply(@event.CardToUpgradeTo, @event.TurnScope);
        }

        public enum State
        {
            Initial,
            MinePlayed,
            CardSelectedToTrash,
            Complete,
        };

        public enum Trigger
        {
            MinePlayed,
            CardDiscardedForMine,
            CardSelectedToTrashForMine,
            DeclinedToTrashCardForMine,
            TreasurePickedToUpgradeToWithMine
        }

        public void Handle(Mine.MinePlayedMessage message)
        {
            RequestSelectionOfTreasureToTrash(message);
        }
    }
}