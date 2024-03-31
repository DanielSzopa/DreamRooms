using BuildingBlocks.Abstractions.Commands;

namespace BuildingBlocks.Commands;

internal interface INotificationOutBoxCommand : ICommand
{
    string Module { get; }
}
