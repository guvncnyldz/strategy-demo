using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class UnitAttackState : UnitFSMBase
{
    private IGridNode _targetNode;
    private IHittable _target;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        _unitController.PathAgent.SafeStop();

        _target = _unitController.CombatSystemBase.GetTarget;

        if (_target != null)
        {
            _targetNode = _target.GetClosestNode(_unitController.PathAgent.CurrentNode);

            if (_targetNode != null)
                _unitController.PathAgent.RotateTo(_targetNode);
        }

    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        _target = _unitController.CombatSystemBase.GetTarget;

        if (_target != null)
        {
            _targetNode = _target.GetClosestNode(_unitController.PathAgent.CurrentNode);

        }

        if (_targetNode != null)
            _unitController.PathAgent.RotateTo(_targetNode);

        _unitController.CombatSystemBase.TryAttack();
    }
}
