using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public abstract class UnitFSMBase : StateMachineBehaviour
{
    protected UnitController _unitController;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        _unitController = animator.GetComponent<UnitController>();
    }
}
