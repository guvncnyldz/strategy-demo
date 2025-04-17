using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class UnitFSMController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private UnitController _unitController;

    public ICommand CurrentCommand { get; private set; }

    public void Initialize(UnitController unitController)
    {
        _unitController = unitController;
    }

    void Update()
    {
        _animator.SetBool("isPeace", _unitController.CombatSystemBase.GetTarget == null);
        _animator.SetBool("isInRange", _unitController.CombatSystemBase.IsInRange());
        _animator.SetBool("isReached", _unitController.PathAgent.IsReached);
    }

    public void SetCommand(ICommand command)
    {
        CurrentCommand = command;

        if (command is AttackCommand)
            _animator.SetTrigger("attackCommand");

        if (command is MoveCommand)
            _animator.SetTrigger("moveCommand");
    }
}
