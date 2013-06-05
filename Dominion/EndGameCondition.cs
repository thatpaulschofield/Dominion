namespace Dominion
{
    public class EndGameCondition
    {
        public bool ConditionMet { get { return IsConditionMet(); } }

        protected virtual bool IsConditionMet()
        {
            return false;
        }
    }
}