using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class ConstructedProductionBuilding : ConstructedBuilding, IProducible
{
    protected SpawnPoint _spawnPoint;

    protected override void Awake()
    {
        base.Awake();
        _spawnPoint = GetComponentInChildren<SpawnPoint>();
    }

    public override void Initialize(BuildingSO buildingSO)
    {
        _spawnPoint.Initialize(buildingSO as ProductionBuildingSO);
        base.Initialize(buildingSO);
    }

    public override void OccupyGrid()
    {
        base.OccupyGrid();

        IGridNode origin = GridManager.Instance.GetGridNodeByWorldPosition(transform.position);

        _spawnPoint.CheckSpawnArea(origin, Vector2Int.one);
        _spawnPoint.OccupySpawnPoint(true);
    }

    public override void ReleaseGrid()
    {
        base.ReleaseGrid();

        _spawnPoint.OccupySpawnPoint(false);
    }

    public List<(string id, string name, Sprite icon)> GetProductList()
    {
        List<(string id, string name, Sprite icon)> values = new List<(string id, string name, Sprite icon)>();

        foreach (UnitSO unit in (_buildingSO as ProductionBuildingSO).Productions)
        {
            values.Add((unit.Id, unit.UnitName, unit.UnitImage));
        }

        return values;
    }

    public void Produce(string id)
    {
        foreach (UnitSO unit in (_buildingSO as ProductionBuildingSO).Productions)
        {
            if (unit.Id == id)
            {
                IGridNode spawnPoint = _spawnPoint.GetSpawnPoint(unit.Prefab);

                if(spawnPoint == null)
                    return;
                    
                UnitController unitController = Services.Get<PoolingService>().Instantiate(unit.Prefab);
                unitController.transform.position = GridManager.Instance.GetWorldPositionByGridNode(spawnPoint);
                unitController.Initialize(unit);
                break;
            }
        }
    }

    public bool IsAvailable(IGridContent gridContent)
    {
        IGridNode gridNode = _spawnPoint.GetSpawnPoint(gridContent);

        return gridNode != null;
    }
}
