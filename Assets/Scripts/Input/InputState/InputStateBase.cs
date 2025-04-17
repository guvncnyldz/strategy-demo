using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class InputStateBase
{
    protected Camera _camera;

    public InputStateBase()
    {
        _camera = Camera.main;
    }
    
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
}
