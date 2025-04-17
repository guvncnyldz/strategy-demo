using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovementState : UnitFSMBase
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        if (_unitController.UnitFSMController.CurrentCommand is MoveCommand moveCommand)
        {
            _unitController.PathAgent.SetDestination(moveCommand.Target);
        }
    }
}
