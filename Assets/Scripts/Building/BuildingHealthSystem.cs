using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHealthSystem : MonoBehaviour
{
    private IHittable _hittable;
    private float _health;

    private DamageAnimation _damageAnimation;

    void Awake()
    {
        _damageAnimation = GetComponentInChildren<DamageAnimation>();
    }

    public void Initialize(IHittable hittable, float health)
    {
        _hittable = hittable;
        _health = health;
    }

    public void Hit(float damage)
    {
        if (_health < 0)
            return;

        _damageAnimation.Play();
        _health -= damage;

        if (_health < 0)
            Die();
    }

    void Die()
    {
        _hittable.OnDeathEvent?.Invoke(_hittable);
        _hittable.Die();
    }
}
