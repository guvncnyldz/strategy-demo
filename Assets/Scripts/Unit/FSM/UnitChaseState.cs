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

    //This is hard coded to make a quick optimization
    float lastDecisionTime;
    float desicionCooldown = 1;

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
            if (hitBox == null)
                continue;

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

        if (lastDecisionTime + desicionCooldown > Time.time)
            return;

        if (_target != null)
        {
            bool hitboxNull = _selectedHitbox == null;
            bool targetNodeNull = _targetNode == null;
            bool targetMoved = !_selectedHitbox.IsOccupiedBy(_target as IGridContent);
            bool targetNodeOccupied = _unitController.PathAgent.CurrentNode != _targetNode && _targetNode.IsOccupiedFor(_unitController);

            bool needsRecalculation = hitboxNull || targetNodeNull || targetMoved || targetNodeOccupied;

            if (needsRecalculation)
            {
                CalculateAttack();

                _unitController.CombatSystemBase.SetHitbox(_selectedHitbox);

                if (_targetNode != null)
                {
                    _unitController.PathAgent.SetDestination(_targetNode);
                    lastDecisionTime = Time.time;
                }
            }
        }
    }
}
