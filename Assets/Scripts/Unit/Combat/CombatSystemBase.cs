using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CombatSystemBase : MonoBehaviour
{
    protected IHittable _target;

    protected UnitController _unitController;
    protected float _range, _cooldown, _lastAttack, _damage;

    public IHittable GetTarget => _target;
    public float GetRange => _range;

    public void Initialize(UnitController unitController, UnitSO unitSO)
    {
        _unitController = unitController;
        _range = unitSO.AttackRange;
        _cooldown = unitSO.AttackCooldown;
        _damage = unitSO.Damage;
    }

    public void SetTarget(IHittable target)
    {
        _target = target;

        _target.OnDeathEvent += OnTargetDeath;
    }

    public bool IsInRange()
    {
        if (_target == null)
            return false;

        float distance = GridManager.Instance.GetWorldDistance(_unitController.PathAgent.CurrentNode, _target.GetClosestNode(_unitController.PathAgent.CurrentNode));

        return distance <= _range;
    }

    public bool TryAttack()
    {
        if (!IsInRange())
            return false;

        if (_lastAttack + _cooldown > Time.time)
            return false;

        Attack();
        _lastAttack = Time.time;

        return true;
    }

    protected virtual void Attack()
    {
        _unitController.UnitAnimationController.AttackAnimation();
    }

    void OnTargetDeath(IHittable target)
    {
        _target.OnDeathEvent -= OnTargetDeath;
        _target = null;
    }
}
