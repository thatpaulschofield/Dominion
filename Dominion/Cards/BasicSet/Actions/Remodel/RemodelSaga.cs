using System;
using Dominion.Cards.BasicSet.Actions.Saga;

namespace Dominion.Cards.BasicSet.Actions.Remodel
{
    public class RemodelSaga : Saga<RemodelSaga.State,RemodelSaga.Trigger>,
        IStartedBy<RemodelPlayedMessage>,
        IRespondTo<CardSelectedToRemodelResponse>,
        IRespondTo<CardSelectedToRemodelToResponse>
    {
        private Card _cardToRemodel;

        public RemodelSaga() : base(State.Initial, State.Complete)
        {
            Configure(State.Initial)
                .PermitDynamic(Event<RemodelPlayedMessage>(), m => State.RemodelPlayed);
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
            message.TurnScope.Publish(new PickCardToRemodelToCommand(message.Card.Cost + 2, message.TurnScope){Id = Guid.NewGuid(), CorrelationId = message.CorrelationId, OriginalEventId = message.OriginalEventId});
        }

        private void PerformRemodel(CardSelectedToRemodelToResponse message)
        {
            message.TurnScope.TrashCard(_cardToRemodel);
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