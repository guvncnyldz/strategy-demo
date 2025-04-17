using System.Collections.Generic;
using UnityEngine.Events;

public interface IHittable
{
    public UnityAction<IHittable> OnDeathEvent { get; set; }
    public List<IGridNode> GetHitBoxes(IGridNode gridNode);

    void Hit(float damage);
    void Die();
}