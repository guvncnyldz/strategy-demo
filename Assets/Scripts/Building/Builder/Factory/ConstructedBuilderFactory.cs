public class ConstructedBuilderFactory : IBuilderFactory
{
    public BuilderBase CreateBuilder(BuildingSO buildingSO)
    {
        if (buildingSO is ProductionBuildingSO)
            return new ConstructedProductionBuilder(buildingSO);

        return new ConstructedBuilder(buildingSO);
    }
}