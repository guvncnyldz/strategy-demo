using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCombatSystem : CombatSystemBase
{
    protected override void Attack()
    {
        base.Attack();
        
        _target.Hit(_damage);
    }
}