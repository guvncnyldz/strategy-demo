public interface IAttackable : ICommandable
{
    public void Attack(IHittable hittable);
    public IGridContent GetGridContent();
}
