using Dominion.GameEvents;

namespace Dominion.Cards.BasicSet.Actions.MineAction
{
    public class GameEventResponse<T> : GameEventResponse
    {
        private T _item;

        public GameEventResponse(ITurnScope turnScope)
            : base(turnScope)
        {
        }

        public GameEventResponse(T item, ITurnScope turnScope) : base(turnScope)
        {
            Item = item;
        }

        public T Item
        {
            get { return _item; }
            set {
                {
                    _item = value;
                    OnItemSet();
                } }
        }

        public GameEventResponse<T> WithItem(T item)
        {
            Item = item;
            return this;
        }

        protected virtual void OnItemSet()
        {
            
        }

        public override void Execute()
        {
            
        }
    }
}