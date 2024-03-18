using BuildingBlocks.Modules;

namespace BuildingBlocks.UnitOfWork;

internal class UnitOfWorkTypeRegistery
{
    private readonly Dictionary<string, Type> _types = new();

    internal void Register<T>() where T : IUnitOfWork => _types[GetKey<T>()] = typeof(T);

    internal Type Resolve<T>() => _types.TryGetValue(GetKey<T>(), out var type) ? type : null;

    private static string GetKey<T>() => $"{typeof(T).GetModuleName()}";
}
