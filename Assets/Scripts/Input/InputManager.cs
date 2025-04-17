using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : SingletonMonoBehaviour<InputManager>
{
    [SerializeField] private InputActionAsset _inputActionMap;

    private InputAction _leftClickAction, _rightClickAction, _middleClickAction;
    private InputStateBase _currentInputState;

    private HashSet<ILeftClickable> _leftClickables = new();
    private HashSet<IRightClickable> _rightClickables = new();
    private HashSet<IMiddleClickable> _middleClickables = new();

    public InputStateBase CurrentState => _currentInputState;

    protected override void Awake()
    {
        base.Awake();

        _inputActionMap.Enable();

        _leftClickAction = _inputActionMap.FindAction("LeftClick");
        _rightClickAction = _inputActionMap.FindAction("RightClick");
        _middleClickAction = _inputActionMap.FindAction("MiddleClick");

        _leftClickAction.performed += OnLeftClickPerformed;
        _rightClickAction.performed += OnRightClickPerformed;
        _middleClickAction.performed += OnMiddleClickPerformed;
    }

    public void Subscribe(IInputTarget inputTarget)
    {
        if (inputTarget is ILeftClickable leftClickable)
            _leftClickables.Add(leftClickable);

        if (inputTarget is IRightClickable rightClickable)
            _rightClickables.Add(rightClickable);

        if (inputTarget is IMiddleClickable middleClickable)
            _middleClickables.Add(middleClickable);
    }

    public void Unsubscribe(IInputTarget inputTarget)
    {
        if (inputTarget is ILeftClickable leftClickable)
            _leftClickables.Remove(leftClickable);

        if (inputTarget is IRightClickable rightClickable)
            _rightClickables.Remove(rightClickable);

        if (inputTarget is IMiddleClickable middleClickable)
            _middleClickables.Remove(middleClickable);
    }

    private void OnLeftClickPerformed(InputAction.CallbackContext context)
    {
        foreach (var leftClickable in _leftClickables.ToArray())
        {
            leftClickable.LeftClick(context);
        }
    }

    private void OnRightClickPerformed(InputAction.CallbackContext context)
    {
        foreach (var rightClickable in _rightClickables.ToArray())
        {
            rightClickable.RightClick(context);
        }
    }

    private void OnMiddleClickPerformed(InputAction.CallbackContext context)
    {
        foreach (var middleClickable in _middleClickables.ToArray())
        {
            middleClickable.MiddleClick(context);
        }
    }

    void Update()
    {
        _currentInputState?.UpdateState();
    }

    public void SetState(InputStateBase inputStateBase)
    {
        _currentInputState?.ExitState();
        _currentInputState = inputStateBase;
        _currentInputState?.EnterState();
    }

    void OnDestroy()
    {
        _leftClickAction.performed -= OnLeftClickPerformed;
        _rightClickAction.performed -= OnRightClickPerformed;
        _middleClickAction.performed -= OnRightClickPerformed;
    }
}
