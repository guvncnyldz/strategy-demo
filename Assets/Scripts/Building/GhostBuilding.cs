
using UnityEngine;

public class GhostBuilding : BuildingBase
{
    public bool IsConstructable;

    void Update()
    {
        IsConstructable = CheckGridAvailable();

        Color color = IsConstructable ? Color.white : Color.red;
        color.a = 0.7f;

        _spriteRenderer.color = color;
    }

    protected virtual bool CheckGridAvailable()
    {
        IGridNode _currentNode = GridManager.Instance.GetGridNodeByWorldPosition(transform.position);
        transform.position = GridManager.Instance.GetWorldPositionByGridNode(_currentNode);

        for (int x = 0; x < _buildingSO.XSize; x++)
        {
            for (int y = 0; y < _buildingSO.YSize; y++)
            {
                if (GridManager.Instance.TryGetGrid(_currentNode.GridPosition.x + x, _currentNode.GridPosition.y + y, out IGridNode gridNode))
                {
                    if (gridNode.IsOccupied(this))
                        return false;
                }
                else
                {
                    return false;
                }
            }
        }

        return true;
    }
}
