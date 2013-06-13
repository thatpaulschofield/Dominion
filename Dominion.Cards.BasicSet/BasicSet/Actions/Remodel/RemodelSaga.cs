using System;
using Dominion.Cards.BasicSet.Actions.Saga;
using Dominion.Cards.BasicSet.BasicSet.Actions.Remodel;
using Dominion.Cards.Saga;
using Stateless;

namespace Dominion.Cards.BasicSet.Actions.Remodel
{
    public static class StateConfigurationExtensions
    {
        public static StateMachine<STATE, TRIGGER>.StateConfiguration TransitionOnEvent<STATE, TRIGGER, TEVENT>(
            this StateMachine<STATE, TRIGGER>.StateConfiguration config, StateMachine<STATE, TRIGGER>.TriggerWithParameters<TEVENT> @event, Func<TEVENT, STATE> newState)
        {
            return config.PermitDynamic(@event, newState);
        }
    }

    public class RemodelSaga : Saga<RemodelSaga.State,RemodelSaga.Trigger>,
        IStartedBy<RemodelPlayedMessage>,
        IRespondTo<CardSelectedToRemodelResponse>,
        IRespondTo<CardSelectedToRemodelToResponse>
    {
        private Card _cardToRemodel;

        public RemodelSaga() : base(State.Initial, State.Complete)
        {
            Configure(State.Initial)
                .TransitionOnEvent(Event<RemodelPlayedMessage>(), m => State.RemodelPlayed);
            Configure(State.RemodelPlayed)
                .OnEntryFrom(Event<RemodelPlayedMessage>(), RequestCardToRemodel)
                .PermitDynamic(Event<CardSelectedToRemodelResponse>(), m => State.CardSelectedToRemodel);
            Configure(State.CardSelectedToRemodel)
                .OnEntryFrom(Event<CardSelectedToRemodelResponse>(), RequestCardToRemodelTo)
                .PermitDynamic(Event<CardSelectedToRemodelToResponse>(), m => State.Complete);
            Configure(State.Complete)
                .OnEntryFrom(Event<CardSelectedToRemodelToResponse>(), PerformRemodel);
            CompletedState = State.Complete;
        }

        private void RequestCardToRemodel(RemodelPlayedMessage message)
        {
            Id = message.CorrelationId;
            message.TurnScope.Publish(new PickCardToRemodelCommand(message.TurnScope){Id = Guid.NewGuid(), CorrelationId = Id, OriginalEventId = message.Id});
        }

        private void RequestCardToRemodelTo(CardSelectedToRemodelResponse message)
        {
            _cardToRemodel = message.Item;
            message.TurnScope.Publish(new PickCardToRemodelToCommand(message.TurnScope.GetPrice(message.Item) + 2.Coins(), message.TurnScope) { Id = Guid.NewGuid(), CorrelationId = message.CorrelationId, OriginalEventId = message.OriginalEventId });
        }

        private void PerformRemodel(CardSelectedToRemodelToResponse message)
        {
            message.TurnScope.TrashCardFromHand(_cardToRemodel);
            message.TurnScope.GainCardFromSupply(message.Item);
        }

        public enum State
        {
            Initial,
            RemodelPlayed,
            CardSelectedToRemodel,
            Complete
        }

        public enum Trigger
        {
            RemodelPlayed,
            CardSelectedToRemodel,
            CardSelectedToRemodelTo
        }

        public void Handle(RemodelPlayedMessage message)
        {
            
        }
    }
}