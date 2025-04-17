using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ConstructedBuilding : BuildingBase, IHittable
{
    public UnityAction<IHittable> OnDeathEvent { get => _onDeathEvent; set => _onDeathEvent = value; }
    private UnityAction<IHittable> _onDeathEvent;

    public List<IGridNode> gridNodeList;

    private DamageAnimation _damageAnimation;

    void Awake()
    {
        _damageAnimation = GetComponentInChildren<DamageAnimation>();
    }

    public IGridNode GetClosestNodeToAttack(IAttackable attackable, IGridNode gridNode, float maximumRange)
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

    public void Hit(float damage)
    {
        _damageAnimation?.Play();
        Debug.Log("Bina damage: " + damage);
    }

    public virtual void OccupyGrid(bool isOccupied)
    {
        gridNodeList = new List<IGridNode>();

        IGridNode currentNode = GridManager.Instance.GetGridNodeByWorldPosition(transform.position);
        transform.position = GridManager.Instance.GetWorldPositionByGridNode(currentNode);

        for (int x = 0; x < _buildingSO.XSize; x++)
        {
            for (int y = 0; y < _buildingSO.YSize; y++)
            {
                if (GridManager.Instance.TryGetGrid(currentNode.GridPosition.x + x, currentNode.GridPosition.y + y, out IGridNode gridNode))
                {
                    gridNodeList.Add(gridNode);
                    gridNode.Occupy(this);
                }
            }
        }
    }

    public IGridNode GetClosestNode(IGridNode gridNode)
    {
        IGridNode closest = null;
        float minDistance = int.MaxValue;

        foreach (IGridNode node in gridNodeList)
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
