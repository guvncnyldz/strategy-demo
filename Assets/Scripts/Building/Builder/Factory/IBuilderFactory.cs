public interface IBuilderFactory
{
    public BuilderBase CreateBuilder(BuildingSO buildingSO);

    public static IBuilderFactory FactoryMethod(BuilderType builderType)
    {
        switch (builderType)
        {
            case BuilderType.constructed:
                return new ConstructedBuilderFactory();
            case BuilderType.ghost:
                return new GhostBuilderFactory();
        }

        return null;
    }
}

public enum BuilderType
{
    constructed,
    ghost
}