using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class UnitAttackState : UnitFSMBase
{
    private IGridNode _hitbox;
    private IHittable _target;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        _unitController.PathAgent.SafeStop();
        _target = _unitController.CombatSystemBase.GetTarget;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (_target != null)
        {
            List<IGridNode> hitboxes = _target.GetHitBoxes(_unitController.PathAgent.CurrentNode);

            if (hitboxes.Count > 0)
            {
                _hitbox = hitboxes[0];
                _unitController.CombatSystemBase.SetHitbox(_hitbox);
            }
        }

        if (_hitbox != null)
            _unitController.PathAgent.RotateTo(_hitbox);

        _unitController.CombatSystemBase.TryAttack();
    }
}
