public static class BuildingDirector
{
    public static BuildingBase Build(BuilderBase builder, int sortingOrder)
    {
        if(builder.TryGetFromPool(out BuildingBase buildingBase))
        {
            return buildingBase;
        }

        return builder.AddVisual(sortingOrder).CreateParent().AddBuildingComponent().Build();
    }
}
