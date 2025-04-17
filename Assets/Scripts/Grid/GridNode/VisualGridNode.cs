using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualGridNode : MonoBehaviour, IGridNode
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public Vector2Int GridPosition => _gridPosition;

    private Vector2Int _gridPosition;
    private GridConfigSO _gridConfigSO;
    private HashSet<IGridContent> _gridContents = new();

    public void Initialize(Vector2Int gridPosition, GridConfigSO gridConfigSO)
    {
        _spriteRenderer.size = new Vector2(GridConfigSO.SIZE_X, GridConfigSO.SIZE_Y);
        _gridPosition = gridPosition;
        _gridConfigSO = gridConfigSO;

        SetWorldPosition();
    }

    void SetWorldPosition()
    {
        Vector2 origin = _gridConfigSO.GetOrigin();

        float xPos = origin.x + (_gridPosition.x * GridConfigSO.SIZE_X);
        float yPos = origin.y + (_gridPosition.y * GridConfigSO.SIZE_Y);

        transform.position = new Vector2(xPos, yPos);
    }

    public void Occupy(IGridContent gridContent)
    {
        if (!CheckContentShare(gridContent))
            return;

        _gridContents.Add(gridContent);
    }

    public void Release(IGridContent gridContent)
    {
        if (_gridContents.Contains(gridContent))
            _gridContents.Remove(gridContent);
    }

    bool CheckContentShare(IGridContent newGridContent)
    {
        foreach (IGridContent gridContent in _gridContents)
        {
            if (!gridContent.IsOcuppyShareAvailable(newGridContent))
            {
                return false;
            }
        }

        return true;
    }

    public bool IsOccupiedFor(IGridContent gridContent = null)
    {
        if (gridContent == null)
            return _gridContents.Count != 0;

        return _gridContents.Count != 0 && !CheckContentShare(gridContent);
    }

    public bool IsOccupiedBy(IGridContent gridContent)
    {
        return _gridContents.Contains(gridContent);
    }

    public HashSet<IGridContent> GetGridContents => _gridContents;
}
