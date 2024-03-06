using BuildingBlocks.Abstractions.Commands;
using Microsoft.EntityFrameworkCore;
using Staff.Core.Domain.Entities;
using Staff.Core.Domain.Exceptions;
using Staff.Core.Domain.ValueObjects;
using Staff.Core.Persistence;
using Staff.Core.Security;

namespace Staff.Core.Features.SignUpReceptionist;

internal class SignUpReceptionistCommandHandler : ICommandHandler<SignUpReceptionistCommand>
{
    private readonly IPasswordManager _passwordManager;
    private readonly StaffDbContext staffDbContext;

    public SignUpReceptionistCommandHandler(IPasswordManager passwordManager, StaffDbContext staffDbContext)
    {
        _passwordManager = passwordManager;
        this.staffDbContext = staffDbContext;
    }

    public async Task HandleAsync(SignUpReceptionistCommand command, CancellationToken cancellationToken = default)
    {
        if (await staffDbContext.Employees.AnyAsync(e => e.Email == new Email(command.Email)))
            throw new EmailAlreadyExistsException(command.Email);

        var receptionist = Employee
            .CreateReceptionist(command.FirstName, command.LastName, command.Email, command.PhoneNumber, command.Password, _passwordManager);
        await staffDbContext.AddAsync(receptionist, cancellationToken);
    }
}
