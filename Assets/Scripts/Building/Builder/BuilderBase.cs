using UnityEngine;

public abstract class BuilderBase
{
    protected abstract string objName { get; }

    protected GameObject _building;
    protected GameObject _buildingVisual;
    protected BuildingBase _buildingBase;
    protected BuildingSO _buildingSO;

    public BuilderBase(BuildingSO buildingSO)
    {
        _buildingSO = buildingSO;
    }

    public virtual BuilderBase AddVisual(int sortingOrder)
    {
        _buildingVisual = new GameObject("GhostVisual");
        var sr = _buildingVisual.AddComponent<SpriteRenderer>();
        sr.sortingOrder = sortingOrder;
        return this;
    }

    public virtual BuilderBase CreateParent()
    {
        _building = new GameObject(objName);
        _buildingVisual.transform.SetParent(_building.transform);
        return this;
    }

    public virtual BuildingBase Build()
    {
        Services.Get<PoolingService>().Register(_buildingBase, objName);

        return _buildingBase;
    }

    public bool TryGetFromPool(out BuildingBase buildingBase)
    {
        if (Services.Get<PoolingService>().IsPoolExist(objName))
        {
            buildingBase = Services.Get<PoolingService>().Instantiate<BuildingBase>(objName);
            return true;
        }

        buildingBase = null;
        return false;
    }

    public abstract BuilderBase AddBuildingComponent();
}
