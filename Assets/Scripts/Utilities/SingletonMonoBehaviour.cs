using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance;

    protected virtual void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this as T;
    }
}
