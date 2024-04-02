namespace BuildingBlocks.Modules;

public static class Extensions
{
    public static string GetModuleName(this Type type)
    {
        if (type?.Namespace is null)
        {
            return string.Empty;
        }

        return type.Namespace
            .Split(".")
            .First();
    }

    public static string GetModuleName(this object obj)
    {
        return obj?.GetType().GetModuleName();
    }
}
