namespace Dominion.GameEvents
{
    public class GameEventResponseWithItem<T> : GameEventResponse
    {
        private T _item;

        public GameEventResponseWithItem(ITurnScope turnScope)
            : base(turnScope)
        {
        }

        public GameEventResponseWithItem(T item, ITurnScope turnScope)
            : base(turnScope)
        {
            Item = item;
        }

        public T Item
        {
            get { return _item; }
            set
            {
                {
                    _item = value;
                    OnItemSet();
                }
            }
        }

        public GameEventResponseWithItem<T> WithItem(T item)
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

    public class GameEventResponseWithItem<TITEM, TINRESPONSETO> :GameEventResponseWithItem<TITEM>
    {
        public GameEventResponseWithItem(ITurnScope turnScope) : base(turnScope)
        {
        }

        public GameEventResponseWithItem(TITEM item, ITurnScope turnScope) : base(item, turnScope)
        {
        }
    }
}