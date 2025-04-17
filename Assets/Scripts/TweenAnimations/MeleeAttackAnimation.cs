using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MeleeAttackAnimation : TweenAnimationBase
{
    void Awake()
    {
        transform.localPosition = Vector2.zero;
    }

    public override void Play()
    {
        if (_isActive)
            return;

        _isActive = true;

        Vector3 direction = transform.localRotation * Vector3.up;
        Vector3 target = transform.localPosition + direction.normalized * 0.075f;

        transform.DOLocalMove(target, 0.075f)
            .SetLoops(2, LoopType.Yoyo)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                _isActive = false;
                transform.localPosition = Vector3.zero;
            });
    }
}
