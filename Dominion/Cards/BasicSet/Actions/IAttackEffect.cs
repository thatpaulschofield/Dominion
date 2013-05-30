namespace Dominion.Cards.BasicSet.Actions
{
    public interface IAttackEffect
    {
        void Resolve(IReactionScope scope);
    }
}