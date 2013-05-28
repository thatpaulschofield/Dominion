namespace Dominion
{
    internal class EndGameCondition
    {
        public bool ConditionMet { get { return IsConditionMet(); } }

        protected virtual bool IsConditionMet()
        {
            return false;
        }
    }
}