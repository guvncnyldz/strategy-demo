public class ConstructedBuilder : BuilderBase
{
    protected override string objName => _buildingSO.BuildingName;

    public ConstructedBuilder(BuildingSO buildingSO) : base(buildingSO)
    {
    }

    public override BuilderBase AddVisual(int sortingOrder)
    {
        base.AddVisual(sortingOrder);
        _buildingVisual.AddComponent<DamageAnimation>();
        return this;
    }
    public override BuilderBase AddBuildingComponent()
    {
        _buildingBase = _building.AddComponent<ConstructedBuilding>();
        return this;
    }
}
