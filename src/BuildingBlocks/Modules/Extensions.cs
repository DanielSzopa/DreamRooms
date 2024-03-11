namespace BuildingBlocks.Modules;

internal static class Extensions
{
    internal static string GetModuleName(this Type type)
    {
        if (type?.Namespace is null)
        {
            return string.Empty;
        }

        return type.Namespace
            .Split(".")
            .First();
    }

    internal static string GetModuleName(this object obj)
    {
        return obj?.GetType().GetModuleName();
    }
}
