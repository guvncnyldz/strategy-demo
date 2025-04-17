using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class UnitChaseState : UnitFSMBase
{
    private IGridNode _selectedHitbox;
    private IGridNode _targetNode;
    private IHittable _target;
    private IGridNode _startGrid;

    private List<IGridNode> _hitBoxes;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        if (_unitController.UnitFSMController.CurrentCommand is AttackCommand attackCommand)
        {
            _startGrid = _unitController.PathAgent.CurrentNode;
            _target = attackCommand.Hittable;

            CalculateAttack();

            _unitController.CombatSystemBase.SetTarget(attackCommand.Hittable);
            _unitController.CombatSystemBase.SetHitbox(_selectedHitbox);

            if (_targetNode != null)
                _unitController.PathAgent.SetDestination(_targetNode);
        }
    }

    void CalculateAttack()
    {
        _hitBoxes = _target.GetHitBoxes(_unitController.PathAgent.CurrentNode);

        foreach (IGridNode hitBox in _hitBoxes)
        {
            IGridNode tempTargetNode = PathFinder.GetValidAttackPosition(hitBox, _startGrid, _unitController.CombatSystemBase.GetRange, _unitController, GridManager.Instance);

            if (tempTargetNode != null)
            {
                _targetNode = tempTargetNode;
                _selectedHitbox = hitBox;
                break;
            }
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (_target != null)
        {
            bool needsRecalculation =
             _selectedHitbox == null || // No attack point has been selected yet
             _targetNode == null || // Target node is not assigned
             !_selectedHitbox.IsOccupiedBy(_target as IGridContent) || // Target is no longer at the selected attack point
             (_unitController.PathAgent.CurrentNode != _targetNode && _targetNode.IsOccupiedFor(_unitController));
            //target node is now occupied by someone else

            if (needsRecalculation)
            {
                CalculateAttack();

                if (_targetNode != null)
                    _unitController.PathAgent.SetDestination(_targetNode);
            }
        }
    }
}
