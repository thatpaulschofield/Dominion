using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominion.Cards.BasicSet.Actions.MineAction;
using Dominion.GameEvents;
using StructureMap;

namespace Dominion.Infrastructure
{
    public class Bus : IBus, IHandleEvents
    {
        private readonly ISagaRepository _repository;
        private readonly IContainer _container;

        public Bus(IEventAggregator eventAggregator, ISagaRepository sagaRepository, IContainer container)
        {
            eventAggregator.Register(this);
            _repository = sagaRepository;
            _container = container;
        }

        public void Handle(IGameMessage @event)
        {
            RouteToNewSagas(@event);
            RouteToExistingSagas(@event);
        }

        private void RouteToExistingSagas(IGameMessage @event)
        {
            try
            {
                var existingSaga = _repository.Find(@event.CorrelationId);
                if (existingSaga != null)
                {
                    try
                    {
                        existingSaga.Handle(@event);
                    }
                    finally
                    {
                        CleanUp(existingSaga);
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void RouteToNewSagas(IGameMessage @event)
        {
            try
            {
                var typeForSagasStartedByEvent = typeof(IStartedBy<>).MakeGenericType(@event.GetType());
                var sagas = _container.GetAllInstances(typeForSagasStartedByEvent).Cast<Saga>().ToList();
                sagas.ForEach(saga => StartSaga(@event, saga));
            }
            catch (Exception)
            {
                
            }
        }

        private void StartSaga(IGameMessage @event, Saga saga)
        {
            _repository.Add(saga);
            try
            {
                saga.Handle(@event);
            }
            finally
            {
                CleanUp(saga);
            }
        }

        private void CleanUp(Saga saga)
        {
            if (saga.IsComplete)
            {
                _repository.Remove(saga);
            }
        }

        public bool CanHandle(IGameMessage @event)
        {
            return true;
        }
    }

    public class SagaRepository : ISagaRepository
    {
        List<Saga> _sagas = new List<Saga>();
        public Saga Find(Guid id)
        {
            if (_sagas.Any(s => s.Id == id))
                return _sagas.Last(s => s.Id.Equals(id));

            return null;
        }

        public void Add(Saga saga)
        {
            _sagas.Add(saga);
        }

        public void Remove(Saga saga)
        {
            _sagas.Remove(saga);
        }
    }
    public interface ISagaRepository
    {
        Saga Find(Guid id);
        void Add(Saga saga);
        void Remove(Saga saga);
    }

    public interface IBus
    {
    }
}
