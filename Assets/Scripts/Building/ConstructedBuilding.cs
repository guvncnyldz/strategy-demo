using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ConstructedBuilding : BuildingBase, IHittable, IInteractable
{
    public UnityAction<IHittable> OnDeathEvent { get => _onDeathEvent; set => _onDeathEvent = value; }
    private UnityAction<IHittable> _onDeathEvent;

    public UnityAction<IInteractable> InteractionInterruptEvent { get => _interruptEvent; set => _interruptEvent = value; }
    private UnityAction<IInteractable> _interruptEvent;

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
        _interruptEvent?.Invoke(this);
        ReleaseGrid();
        Services.Get<PoolingService>().Destroy(this);
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

    public List<IGridNode> GetHitBoxes(IGridNode gridNode)
    {
        return new List<IGridNode>(_gridNodeList
         .OrderBy(node => GridManager.Instance.GetWorldDistance(node, gridNode)));
    }
}
