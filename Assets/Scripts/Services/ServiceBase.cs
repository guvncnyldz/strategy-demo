using System;
using UnityEngine;

public class ServiceBase : MonoBehaviour
{
    private bool isRegistered;
    
    protected virtual void Awake()
    {
        if (isRegistered)
            return;

        isRegistered = true;

        Type type = GetType();

        Services.Register(type, this);
    }

    protected virtual void OnDestroy()
    {
        if (!isRegistered)
            return;

        Type type = GetType();

        Services.Unregister(type);

        foreach (var @interface in type.GetInterfaces())
        {
            Services.Unregister(@interface);
        }
    }
}
