using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class MonoBehaviourExtensions
{
    public static Coroutine WaitForSingleFrame(this MonoBehaviour mb, UnityAction action)
    {
        Coroutine coroutine = mb.StartCoroutine(Delay());

        IEnumerator Delay()
        {
            yield return null;
            action.Invoke();
        }

        return coroutine;
    }

    public static Coroutine WaitForSeconds(this MonoBehaviour mb, UnityAction action, float seconds)
    {
        Coroutine coroutine = mb.StartCoroutine(Delay());

        IEnumerator Delay()
        {
            yield return new WaitForSeconds(seconds);
            action.Invoke();
        }

        return coroutine;
    }
}
