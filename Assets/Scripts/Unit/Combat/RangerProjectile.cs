using DG.Tweening;
using UnityEngine;

public class RangerProjectile : MonoBehaviour
{
    private IGridNode _target;
    private float _damage;

    public void Initialize(IGridNode target, float damage)
    {
        _target = target;
        _damage = damage;
        
        Vector2 originPos = transform.position;
        Vector2 targetPos = GridManager.Instance.GetWorldPositionByGridNode(target);

        Vector2 direction = (targetPos - originPos).normalized;

        transform.up = direction;

        transform.DOMove(targetPos, 3).SetSpeedBased(true).SetEase(Ease.Linear).OnComplete(() =>
        {
            if (_target.ExtractInterface(out IHittable hittable))
            {
                hittable.Hit(_damage);
            }

            Die();
        });
    }

    void Die()
    {
        Services.Get<PoolingService>().Destroy(this);
    }
}