public class GhostBuilder : BuilderBase
{
    protected override string objName => "GhostBuilding";

    public GhostBuilder(BuildingSO buildingSO) : base(buildingSO)
    {
    }

    public override BuilderBase AddBuildingComponent()
    {
        _buildingBase = _building.AddComponent<GhostBuilding>();
        return this;
    }
}
