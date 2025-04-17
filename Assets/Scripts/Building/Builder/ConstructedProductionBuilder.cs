using UnityEngine;

public class ConstructedProductionBuilder : BuilderBase
{
    protected override string objName => _buildingSO.BuildingName;

    protected GameObject _spawnPoint;

    public ConstructedProductionBuilder(BuildingSO buildingSO) : base(buildingSO)
    {
    }

    public override BuilderBase AddVisual(int sortingOrder)
    {
        base.AddVisual(sortingOrder);

        _spawnPoint = new GameObject("GhostSpawnPoint");

        var sr = _spawnPoint.AddComponent<SpriteRenderer>();
        sr.sortingOrder = sortingOrder;

        _spawnPoint.AddComponent<SpawnPoint>();
        _buildingVisual.AddComponent<DamageAnimation>();

        return this;
    }

    public override BuilderBase CreateParent()
    {
        base.CreateParent();
        _spawnPoint.transform.SetParent(_building.transform);
        return this;
    }

    public override BuilderBase AddBuildingComponent()
    {
        _buildingBase = _building.AddComponent<ConstructedProductionBuilding>();
        return this;
    }
}