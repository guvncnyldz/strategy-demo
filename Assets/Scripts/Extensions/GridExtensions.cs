public static class GridExtensions
{
    public static bool ExtractInterface<T>(this IGridNode gridNode, out T @interface) where T : class
    {
        @interface = null;

        if (gridNode == null || gridNode.GetGridContents.Count == 0)
            return false;

        foreach (IGridContent content in gridNode.GetGridContents)
        {
            if (content is T t)
            {
                @interface = t;
                return true;
            }
        }

        return false;
    }
}