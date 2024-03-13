using BuildingBlocks.Modules;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Persistence;
public class DbContextTypeRegistery
{
    private readonly Dictionary<string, Type> _types = new();

    public void Register<T>()
        where T : DbContext
    {
        _types[GetKey<T>()] = typeof(T);
    }

    public Type Resolve<T>()
    {
        return _types.TryGetValue(GetKey<T>(), out var type) ? type : null;
    }

    private static string GetKey<T>() => $"{typeof(T).GetModuleName()}";
}
