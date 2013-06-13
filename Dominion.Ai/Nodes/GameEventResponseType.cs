using System;
using Dominion.GameEvents;

namespace Dominion.Ai.Nodes
{
    public abstract class EventResponseCriteria
    {
        public Type Type { get; set; }

        public string Name { get { return Type.Name; } }

        public abstract bool IsMatch(IGameMessage message);
    }

    public class EventResponseCriteria<TINRESPONSETO, TRESPONSE> : EventResponseCriteria where TRESPONSE : GameEventResponse<TINRESPONSETO> 
    {
        public override bool IsMatch(IGameMessage message)
        {
            return typeof (GameEventResponse<TINRESPONSETO>).IsAssignableFrom(message.GetType());
        }
    }

    public class EventResponseWithItemCriteria<TINRESPONSETO, TRESPONSE, TITEM> : EventResponseCriteria where TRESPONSE : GameEventResponseWithItem<TINRESPONSETO, TITEM> 
    {
        private readonly TITEM _item;

        protected EventResponseWithItemCriteria(TITEM item)
        {
            _item = item;
        }

        public virtual bool IsMatch(TRESPONSE message)
        {
            return message.Item.Equals(_item);
        }

        public override bool IsMatch(IGameMessage message)
        {
            return IsMatch((TRESPONSE) message);
        }
    }
}