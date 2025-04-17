using UnityEngine.InputSystem;

public interface IMiddleClickable : IInputTarget
{
    public void MiddleClick(InputAction.CallbackContext context);
}