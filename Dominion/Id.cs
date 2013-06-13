namespace Dominion
{
    public abstract class Id<T>
    {
        protected readonly T _id;

        protected Id()
        {
            _id = default(T);
        }

        protected Id(T id)
        {
            _id = id;
        }

        public override string ToString()
        {
            return _id.ToString();
        }

        public static implicit operator T(Id<T> id)
        {
            return id._id;
        }
    }
}