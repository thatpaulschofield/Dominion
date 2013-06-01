using System;
using System.Linq;
using Dominion.GameEvents;

namespace Dominion.Cards.BasicSet.Actions.MineAction
{
    public class MineSaga : Saga<MineSaga.State, MineSaga.Trigger>, 
        IStartedBy<MinePlayedMessage>,
        IRespondTo<CardTrashedForMineEvent>
        
    {
        public MineSaga(IEventAggregator eventAggregator) : base(eventAggregator, State.Initial)
        {
            Configure(State.Initial)
                .PermitDynamic(Event<MinePlayedMessage>(Trigger.MinePlayed), m => State.MinePlayed);
            Configure(State.MinePlayed)
                .OnEntryFrom(Event<MinePlayedMessage>(Trigger.MinePlayed), MinePlayed)
                .Permit(Trigger.CardTrashedForMine, State.WaitingForUpgradeSelection)
                .Permit(Trigger.DeclinedToTrashCardForMine, State.Complete);
            Configure(State.WaitingForUpgradeSelection)
                .OnEntryFrom(Event<CardTrashedForMineEvent>(Trigger.CardTrashedForMine), SelectCardToUpgradeTo)
                .Permit(Trigger.CardSelectedToUpgradeTo, State.Complete);
            Configure(State.Complete)
                .OnEntryFrom(Event<DeclinedToTrashCardForMineResponse>(Trigger.DeclinedToTrashCardForMine),DeclinedToTrashCard);
            CompletedState = State.Complete;
        }

        private void MinePlayed(MinePlayedMessage message)
        {
            Id = message.CorrelationId;
            message.TurnScope.Publish(new PickTreasureToTrashForMineCommand(message.TurnScope) { Id = Id, CorrelationId = Id, OriginalEventId = Id });
        }

        private void SelectCardToUpgradeTo(CardTrashedForMineEvent @event)
        {
            _eventAggregator.Publish(new PickTreasureToUpgradeToForMineCommand(@event.TrashedCard.Cost + 3, @event.TurnScope){ Id = Guid.NewGuid(), CorrelationId = @event.OriginalEventId, OriginalEventId = @event.OriginalEventId});            
        }

        private void DeclinedToTrashCard(DeclinedToTrashCardForMineResponse @event)
        {
            
        }

        public enum State
        {
            Initial,
            MinePlayed,
            CardTrashed,
            Complete,
            WaitingForUpgradeSelection
        };

        public enum Trigger
        {
            MinePlayed,
            CardDiscardedForMine,
            CardTrashedForMine,
            DeclinedToTrashCardForMine,
            CardSelectedToUpgradeTo
        }

        public void Handle(MinePlayedMessage message)
        {
            MinePlayed(message);
        }
    }

    internal class PickTreasureToUpgradeToForMineCommand : GameMessage
    {
        private Money _cost;

        public PickTreasureToUpgradeToForMineCommand(Money cost, ITurnScope scope) : base(scope)
        {
            _cost = cost;
            Description = String.Format("Pick a treasure costing {0} to upgrade to [Mine]", cost);
            GetAvailableResponses =
                () =>
                scope.Supply.FindCardsCostingUpTo(cost)
                     .Treasures()
                     .Select(t => new TreasurePickedToUpgradeToWithMine(t, scope));
        }
    }

    public class TreasurePickedToUpgradeToWithMine : GameEventResponse
    {
        private readonly Card _card;

        public TreasurePickedToUpgradeToWithMine(Card card, ITurnScope scope) : base(scope)
        {
            _card = card;
            Description = String.Format("Upgrade to {0}", card.Name);
        }

        public override void Execute()
        {
            TurnScope.GainCardFromSupply(_card);
        }
    }

    public interface IRespondTo<T>
    {
    }
}