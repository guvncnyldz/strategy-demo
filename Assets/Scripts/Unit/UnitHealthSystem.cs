using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealthSystem : MonoBehaviour
{
    private IHittable _hittable;
    private float _health;
    private UnitController _unitController;

    private DamageAnimation _damageAnimation;

    void Awake()
    {
        _damageAnimation = GetComponentInChildren<DamageAnimation>();
    }

    public void Initialize(UnitController unitController, float health)
    {
        _unitController = unitController;
        _hittable = unitController;
        _health = health;
    }

    public void Hit(float damage)
    {
        if (_health < 0)
            return;

        _unitController.UnitAnimationController.DamageAnimation();

        _health -= damage;

        if (_health <= 0)
            Die();
    }

    void Die()
    {
        _hittable.OnDeathEvent?.Invoke(_hittable);
        _hittable.Die();
    }
}
