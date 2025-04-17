using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InteractionService : ServiceBase, ILeftClickable
{
    public UnityAction<IInteractable> InteractionEvent;
    public UnityAction<ICommandable> CommandableEvent;

    private Camera _camera;

    void Start()
    {
        _camera = Camera.main;
        InputManager.Instance.Subscribe(this);
    }

    public void LeftClick(InputAction.CallbackContext context)
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        Vector3 mousePosition = _camera.ScreenToWorldPoint(context.ReadValue<Vector2>());
        mousePosition.z = 0;
        IGridNode gridNode = GridManager.Instance.GetGridNodeByWorldPosition(mousePosition);

        if (gridNode.ExtractInterface(out IInteractable interactable))
        {
            InteractionEvent?.Invoke(interactable);
        }
        else
        {
            InteractionEvent?.Invoke(null);
        }

        if (gridNode.ExtractInterface(out ICommandable commandable))
        {
            InputManager.Instance.SetState(new CommandingInputState(commandable));
            return;
        }
    }

    protected override void OnDestroy()
    {
        InputManager.Instance.Unsubscribe(this);

        base.OnDestroy();
    }
}