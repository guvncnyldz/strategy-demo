using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IGridNode
{
    public Vector2Int GridPosition { get; }
    public HashSet<IGridContent> GetGridContents { get; }

    public void Initialize(Vector2Int gridPosition, GridConfigSO _gridConfigSO);
    public bool IsOccupiedFor(IGridContent gridContent);
    public bool IsOccupiedBy(IGridContent gridContent);
    public void Occupy(IGridContent gridContent);
    public void Release(IGridContent gridContent);
}
