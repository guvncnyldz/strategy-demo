using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DamageAnimation : TweenAnimationBase
{
    private SpriteRenderer _spriteRenderer;
    private Color _defaultColor;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultColor = Color.white;
    }

    public override void Play()
    {
        if (_isActive)
            return;

        _isActive = true;

        _spriteRenderer.DOColor(Color.red, 0.1f).OnComplete(() =>
        {
            _spriteRenderer.DOColor(_defaultColor, 0.2f).OnComplete(() =>
            {
                _isActive = false;
            });
        });
    }
}
