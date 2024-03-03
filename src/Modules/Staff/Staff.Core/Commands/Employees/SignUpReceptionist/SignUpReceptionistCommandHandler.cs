using BuildingBlocks.Abstractions.Commands;

namespace Staff.Core.Commands.Employees.SignUpReceptionist;

internal class SignUpReceptionistCommandHandler : ICommandHandler<SignUpReceptionistCommand>
{
    public Task HandleAsync(SignUpReceptionistCommand command, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
