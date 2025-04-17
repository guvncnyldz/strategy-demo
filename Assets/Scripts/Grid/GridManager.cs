using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GridManager : SingletonMonoBehaviour<GridManager>, IGrid
{
    [SerializeField] private GridConfigSO _gridConfigSO;

    private IGridNode[,] _gridNodeArray;

    public float GridWidthWorld => _gridConfigSO.Count.x * GridConfigSO.SIZE_X;
    public float GridHeightWorld => _gridConfigSO.Count.y * GridConfigSO.SIZE_Y;

    public IGridNode[,] GetAllNodes => _gridNodeArray;

    protected override void Awake()
    {
        base.Awake();

        GenerateGrid();
    }

    public void GenerateGrid()
    {
        GridGenerator gridGenerator = new GridGenerator(_gridConfigSO);
        _gridNodeArray = gridGenerator.Generate();
    }

    public IGridNode GetGridNodeByWorldPosition(Vector2 worldPos, bool closest = true)
    {
        Vector2 origin = _gridConfigSO.GetOrigin();

        int x = Mathf.RoundToInt((worldPos.x - origin.x) / GridConfigSO.SIZE_X);
        int y = Mathf.RoundToInt((worldPos.y - origin.y) / GridConfigSO.SIZE_Y);

        if (TryGetGrid(x, y, out IGridNode node))
            return node;

        if (!closest)
            return null;

        //If we can not get, get closest
        int clampedX = Mathf.Clamp(x, 0, _gridNodeArray.GetLength(0) - 1);
        int clampedY = Mathf.Clamp(y, 0, _gridNodeArray.GetLength(1) - 1);

        return _gridNodeArray[clampedX, clampedY];
    }

    public Vector2 GetWorldPositionByGridNode(IGridNode gridNode)
    {
        Vector2 origin = _gridConfigSO.GetOrigin();
        Vector2 cellSize = new Vector2(GridConfigSO.SIZE_X, GridConfigSO.SIZE_Y);

        float worldX = origin.x + (gridNode.GridPosition.x * cellSize.x);
        float worldY = origin.y + (gridNode.GridPosition.y * cellSize.y);

        return new Vector2(worldX, worldY);
    }

    public bool TryGetGrid(int x, int y, out IGridNode node)
    {
        if (x < 0 || x >= _gridNodeArray.GetLength(0) || y < 0 || y >= _gridNodeArray.GetLength(1))
        {
            node = null;
            return false;
        }

        node = _gridNodeArray[x, y];
        return true;
    }

    public List<IGridNode> GetNeighbours(IGridNode node, bool isCross = false)
    {
        List<IGridNode> neighbors = new List<IGridNode>();
        Span<Vector2Int> directions = isCross
            ? stackalloc Vector2Int[]
            {
            Vector2Int.up,
            Vector2Int.up + Vector2Int.left,
            Vector2Int.up + Vector2Int.right,
            Vector2Int.down,
            Vector2Int.down + Vector2Int.left,
            Vector2Int.down + Vector2Int.right,
            Vector2Int.left,
            Vector2Int.right
            }
            : stackalloc Vector2Int[]
            {
            Vector2Int.up,
            Vector2Int.down,
            Vector2Int.left,
            Vector2Int.right
            };

        foreach (Vector2Int dir in directions)
        {
            Vector2Int neighborPos = node.GridPosition + dir;

            if (TryGetGrid(neighborPos.x, neighborPos.y, out IGridNode gridNode))
            {
                neighbors.Add(gridNode);
            }
        }

        return neighbors;
    }

    public float GetWorldDistance(IGridNode a, IGridNode b)
    {
        return Vector2.Distance(a.GridPosition, b.GridPosition);
    }

    public int GetGridDistance(IGridNode a, IGridNode b)
    {
        return Mathf.Abs(a.GridPosition.x - b.GridPosition.x) + Mathf.Abs(a.GridPosition.y - b.GridPosition.y);
    }
}
