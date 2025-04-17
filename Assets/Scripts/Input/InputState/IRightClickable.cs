using UnityEngine.InputSystem;

public interface IRightClickable : IInputTarget
{
    public void RightClick(InputAction.CallbackContext context);
}