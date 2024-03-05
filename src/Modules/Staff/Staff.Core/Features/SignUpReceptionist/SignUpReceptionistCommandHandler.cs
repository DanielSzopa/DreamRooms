using BuildingBlocks.Abstractions.Commands;
using Staff.Core.Domain.Entities;
using Staff.Core.Security;

namespace Staff.Core.Features.SignUpReceptionist;

internal class SignUpReceptionistCommandHandler : ICommandHandler<SignUpReceptionistCommand>
{
    private readonly IPasswordManager _passwordManager;

    public SignUpReceptionistCommandHandler(IPasswordManager passwordManager)
    {
        _passwordManager = passwordManager;
    }

    public Task HandleAsync(SignUpReceptionistCommand command, CancellationToken cancellationToken = default)
    {
        //Check whether email already exist
        var receptionist = Employee
            .CreateReceptionist(command.FirstName, command.LastName, command.Email, command.PhoneNumber, command.Password, _passwordManager);
        //Add to db
        return Task.CompletedTask;
    }
}
