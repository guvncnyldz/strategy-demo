using UnityEngine;

public class GhostProductionBuilding : GhostBuilding
{
    protected SpawnPoint _spawnPoint;

    void Awake()
    {
        _spawnPoint = GetComponentInChildren<SpawnPoint>();
    }

    public override void Initialize(BuildingSO buildingSO)
    {
        _spawnPoint.Initialize(buildingSO as ProductionBuildingSO);
        base.Initialize(buildingSO);
    }

    protected override bool CheckGridAvailable()
    {
        bool isAvailable = base.CheckGridAvailable();

        IGridNode origin = GridManager.Instance.GetGridNodeByWorldPosition(transform.position);

        isAvailable = isAvailable && _spawnPoint.CheckSpawnArea(origin, Vector2Int.one);
        
        return isAvailable;
    }
}