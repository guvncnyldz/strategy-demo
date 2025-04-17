using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PathAgent : MonoBehaviour
{
    private PathFinder _pathFinder;
    private IGridNode _currentTarget;
    private IGridNode _currentNode;
    private IGridContent _gridContent;

    private float _speed;
    private bool _isStopped;

    public bool IsReached { get; private set; }
    public IGridNode CurrentNode { get => _currentNode; }
    public bool IsPathSuccessful { get => _pathFinder.IsPathSuccessful; }

    public void Initialize(IGridContent gridContent, UnitSO unitSO)
    {
        _speed = unitSO.Speed;

        _gridContent = gridContent;
        _pathFinder = new PathFinder(GridManager.Instance, _gridContent);

        SetCurrentNode(GridManager.Instance.GetGridNodeByWorldPosition(transform.position));
        transform.position = GridManager.Instance.GetWorldPositionByGridNode(CurrentNode);
    }

    public void SetDestination(IGridNode target)
    {
        IsReached = false;
        _currentTarget = target;
        _pathFinder.FindPath(GridManager.Instance.GetGridNodeByWorldPosition(transform.position), _currentTarget);

        BeginMove();
    }

    public void SetDestination(Vector3 target)
    {
        IsReached = false;
        _currentTarget = GridManager.Instance.GetGridNodeByWorldPosition(target);
        _pathFinder.FindPath(GridManager.Instance.GetGridNodeByWorldPosition(transform.position), _currentTarget);

        BeginMove();
    }

    void BeginMove()
    {
        if (_pathFinder.IsPathSuccessful)
        {
            if (_pathFinder.Path.Count >= 2)
            {
                Stop();
                StartCoroutine(Move());
            }
            else
                IsReached = true;
        }
    }

    IEnumerator Move()
    {
        _isStopped = false;

        SetCurrentNode(_pathFinder.Path[1]);
        Vector3 currentWaypoint = GridManager.Instance.GetWorldPositionByGridNode(_pathFinder.Path[1]);

        while (transform.position != currentWaypoint)
        {
            yield return null;
            Vector3 direction = (currentWaypoint - transform.position).normalized;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _speed * 10 * Time.deltaTime);
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, _speed * Time.deltaTime);
        }

        // After reaching each node, recalculate the path in case anything has changed in the grid
        if (!_isStopped)
            SetDestination(_currentTarget);
    }

    public void RotateTo(IGridNode gridNode)
    {
        Vector3 target = GridManager.Instance.GetWorldPositionByGridNode(gridNode);
        Vector3 direction = (target - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _speed * 10 * Time.deltaTime);
    }

    public void Stop()
    {
        StopAllCoroutines();
    }

    public void SetCurrentNode(IGridNode gridNode)
    {
        CurrentNode?.Release(_gridContent);
        _currentNode = gridNode;
        CurrentNode?.Occupy(_gridContent);
    }

    public void SafeStop()
    {
        _isStopped = true;
    }

    public void Die()
    {
        StopAllCoroutines();
        SetCurrentNode(null);
    }
}
