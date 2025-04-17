using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RangeAttackAnimation : TweenAnimationBase
{
    void Awake()
    {
        transform.localScale = Vector3.one;
    }

    public override void Play()
    {
        if (_isActive)
            return;

        _isActive = true;

        transform.DOScale(1.1f, 0.075f)
            .SetEase(Ease.OutQuad)
            .SetLoops(2, LoopType.Yoyo)
            .OnComplete(() =>
            {
                _isActive = false;
                transform.localScale = Vector3.one;
            });
    }
}