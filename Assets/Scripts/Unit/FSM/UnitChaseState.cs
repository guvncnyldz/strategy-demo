using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class UnitChaseState : UnitFSMBase
{
    IGridNode _attackNode;
    IGridNode _targetNode;
    IHittable _target;
    IGridNode _startGrid;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        _startGrid = _unitController.PathAgent.CurrentNode;

        if (_unitController.UnitFSMController.CurrentCommand is AttackCommand attackCommand)
        {
            _unitController.CombatSystemBase.SetTarget(attackCommand.Hittable);

            _target = attackCommand.Hittable;

            _attackNode = _target.GetClosestNodeToAttack(_unitController as IAttackable, _unitController.PathAgent.CurrentNode, _unitController.CombatSystemBase.GetRange);
            _targetNode = _target.GetClosestNode(_unitController.PathAgent.CurrentNode);

            _target.OnDeathEvent += OnTargetDeath;

            if (_attackNode != null)
                _unitController.PathAgent.SetDestination(_attackNode);
        }
    }

    void OnTargetDeath(IHittable target)
    {
        if (target != null)
        {
            _target.OnDeathEvent -= OnTargetDeath;
            _target = null;
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (_target != null)
        {
            IGridNode tempTargetNode = _target.GetClosestNode(_startGrid);

            //Conditions to find new attack point
            if (tempTargetNode != _targetNode
            || _attackNode == null ||
            (_unitController.PathAgent.CurrentNode != _attackNode && _attackNode.IsOccupied(_unitController))
            || !_unitController.PathAgent.IsPathSuccessful)
            {
                _targetNode = tempTargetNode;

                IGridNode tempAttackNode = _target.GetClosestNodeToAttack(_unitController as IAttackable, _unitController.PathAgent.CurrentNode, _unitController.CombatSystemBase.GetRange);

                _attackNode = tempAttackNode;

                if (_attackNode != null)
                    _unitController.PathAgent.SetDestination(_attackNode);
            }
        }
    }
}
