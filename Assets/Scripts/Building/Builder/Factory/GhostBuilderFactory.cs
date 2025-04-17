public class GhostBuilderFactory : IBuilderFactory
{
    public BuilderBase CreateBuilder(BuildingSO buildingSO)
    {
        if (buildingSO is ProductionBuildingSO)
            return new GhostProductionBuilder(buildingSO);

        return new GhostBuilder(buildingSO);
    }
}
