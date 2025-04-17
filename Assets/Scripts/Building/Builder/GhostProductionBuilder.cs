using UnityEngine;

public class GhostProductionBuilder : BuilderBase
{
    protected override string objName => "GhostProductionBuilding";

    protected GameObject _spawnPoint;

    public GhostProductionBuilder(BuildingSO buildingSO) : base(buildingSO)
    {
    }

    public override BuilderBase AddVisual(int sortingOrder)
    {
        _spawnPoint = new GameObject("SpawnPoint");
        var sr = _spawnPoint.AddComponent<SpriteRenderer>();
        sr.sortingOrder = sortingOrder;
        _spawnPoint.AddComponent<SpawnPoint>();

        return base.AddVisual(sortingOrder);
    }

    public override BuilderBase CreateParent()
    {
        base.CreateParent();
        _spawnPoint.transform.SetParent(_building.transform);
        return this;
    }

    
    public override BuilderBase AddBuildingComponent()
    {
        _buildingBase = _building.AddComponent<GhostProductionBuilding>();
        return this;
    }
}
