using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TweenAnimationBase : MonoBehaviour
{
    protected bool _isActive;

    public abstract void Play();
}
