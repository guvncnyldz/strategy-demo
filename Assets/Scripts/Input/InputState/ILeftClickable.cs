using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface ILeftClickable : IInputTarget
{
    public void LeftClick(InputAction.CallbackContext context);
}