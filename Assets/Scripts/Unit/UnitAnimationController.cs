using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimationController : MonoBehaviour
{
    [SerializeField] private TweenAnimationBase _damageAnimation, _attackAnimation;

    public void DamageAnimation()
    {
        _damageAnimation.Play();
    }

    public void AttackAnimation()
    {
        _attackAnimation.Play();
    }
}
