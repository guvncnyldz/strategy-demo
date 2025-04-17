using UnityEngine.Events;

public interface IHittable
{
    public UnityAction<IHittable> OnDeathEvent { get; set; }
    public IGridNode GetClosestNodeToBeAttacked(IAttackable attackable, IGridNode gridNode, float maximumRange);
    public IGridNode GetClosestNode(IGridNode gridNode);
    void Hit(float damage);
    void Die();
}