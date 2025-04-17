using UnityEngine;
using UnityEngine.InputSystem;

public class CommandingInputState : InputStateBase, IRightClickable
{
    private ICommandable _commandable;

    public CommandingInputState(ICommandable commandable) : base()
    {
        _commandable = commandable;
    }

    public override void EnterState()
    {
        InputManager.Instance.Subscribe(this);
    }

    public void RightClick(InputAction.CallbackContext context)
    {
        Vector2 mousePosition = _camera.ScreenToWorldPoint(context.ReadValue<Vector2>());
        IGridNode gridNode = GridManager.Instance.GetGridNodeByWorldPosition(mousePosition, false);

        if (gridNode == null)
            return;

        if (_commandable is IGridContent gridContent)
        {
            if (_commandable is IMoveable moveable && !gridNode.IsOccupiedFor(gridContent))
            {
                moveable.Move(gridNode);
            }
            else if (_commandable is IAttackable attackable && gridNode.ExtractInterface(out IHittable hittable) && _commandable != hittable)
            {
                attackable.Attack(hittable);
            }
        }

    }

    public override void UpdateState()
    {
    }

    public override void ExitState()
    {
        InputManager.Instance.Unsubscribe(this);
    }
}

