using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ConstructedBuilding : BuildingBase, IHittable, IGridContent, IInteractable
{
    public UnityAction<IHittable> OnDeathEvent { get => _onDeathEvent; set => _onDeathEvent = value; }
    private UnityAction<IHittable> _onDeathEvent;

    protected List<IGridNode> _gridNodeList;
    protected BuildingHealthSystem _healthSystem;

    protected virtual void Awake()
    {
        _gridNodeList = new List<IGridNode>();
        _healthSystem = GetComponent<BuildingHealthSystem>();
    }

    public override void Initialize(BuildingSO buildingSO)
    {
        base.Initialize(buildingSO);
        _healthSystem.Initialize(this, _buildingSO.HitPoint);
    }

    public (string, Sprite) GetInformation()
    {
        return (_buildingSO.BuildingName, _buildingSO.BuildingImage);
    }

    public void Hit(float damage)
    {
        _healthSystem.Hit(damage);
    }

    public void Die()
    {
        ReleaseGrid();
        Services.Get<PoolingService>().Destroy(this);
    }

    public IGridNode GetClosestNodeToBeAttacked(IAttackable attackable, IGridNode gridNode, float maximumRange)
    {
        Vector2Int origin = GridManager.Instance.GetGridNodeByWorldPosition(transform.position).GridPosition;

        IGridNode selectedNode = null;
        float closestDistance = float.MaxValue;

        for (int x = origin.x - 1; x < origin.x + 1 + _buildingSO.XSize; x++)
        {
            for (int y = origin.y - 1; y < origin.y + 1 + _buildingSO.YSize; y++)
            {
                int xSize = origin.x + _buildingSO.XSize;
                int ySize = origin.y + _buildingSO.YSize;

                bool isInside = x >= origin.x && x < xSize && y >= origin.y && y < ySize;

                if (isInside) continue;

                if (GridManager.Instance.TryGetGrid(x, y, out IGridNode node))
                {
                    selectedNode = node;

                    if (!node.IsOccupied(attackable.GetGridContent()))
                    {
                        float distance = GridManager.Instance.GetWorldDistance(gridNode, node);

                        if (distance < closestDistance)
                        {
                            closestDistance = distance;
                            selectedNode = node;
                        }
                    }
                }
            }
        }

        return selectedNode;
    }

    public virtual void OccupyGrid()
    {
        IGridNode currentNode = GridManager.Instance.GetGridNodeByWorldPosition(transform.position);
        transform.position = GridManager.Instance.GetWorldPositionByGridNode(currentNode);

        for (int x = 0; x < _buildingSO.XSize; x++)
        {
            for (int y = 0; y < _buildingSO.YSize; y++)
            {
                if (GridManager.Instance.TryGetGrid(currentNode.GridPosition.x + x, currentNode.GridPosition.y + y, out IGridNode gridNode))
                {
                    _gridNodeList.Add(gridNode);
                    gridNode.Occupy(this);
                }
            }
        }
    }

    public virtual void ReleaseGrid()
    {
        foreach (IGridNode gridNode in _gridNodeList)
        {
            gridNode.Release(this);
        }

        _gridNodeList.Clear();
    }

    public IGridNode GetClosestNode(IGridNode gridNode)
    {
        IGridNode closest = null;
        float minDistance = int.MaxValue;

        foreach (IGridNode node in _gridNodeList)
        {
            float distance = GridManager.Instance.GetWorldDistance(gridNode, node);

            if (distance < minDistance)
            {
                minDistance = distance;
                closest = node;
            }
        }

        return closest;
    }
}
