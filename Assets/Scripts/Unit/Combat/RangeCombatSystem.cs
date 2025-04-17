using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeCombatSystem : CombatSystemBase
{
    [SerializeField] private RangerProjectile _projectilePrefab;

    protected override void Attack()
    {
        RangerProjectile projectile = Services.Get<PoolingService>().Instantiate(_projectilePrefab);
        projectile.transform.position = transform.position;

        projectile.Initialize(_hitbox, _damage);
    }
}
