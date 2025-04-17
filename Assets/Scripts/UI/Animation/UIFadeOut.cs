using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UIFadeOut : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private Direction _direction;

    private CanvasGroup _canvasGroup;

    private enum Direction
    {
        Up, Left, Right, Down, Center
    }

    private Coroutine fadeInCoroutine;

    void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Transition()
    {
        if (fadeInCoroutine != null)
            StopCoroutine(fadeInCoroutine);

        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;

        transform.DOKill(true);
        _canvasGroup.DOKill(true);

        fadeInCoroutine = this.WaitForSingleFrame(() =>
        {
            Vector3 startPosition = transform.position;
            Vector3 targetPosition = startPosition + GetDirection() * 250;

            transform.DOMove(targetPosition, _duration);
            _canvasGroup.DOFade(0, _duration).OnComplete(() =>
            {
                transform.position = startPosition;
            });
        });
    }

    Vector3 GetDirection()
    {
        switch (_direction)
        {
            case Direction.Up: return Vector3.up;
            case Direction.Left: return Vector3.left;
            case Direction.Down: return Vector3.down;
            case Direction.Right: return Vector3.right;
        }

        return Vector3.zero;
    }
}
