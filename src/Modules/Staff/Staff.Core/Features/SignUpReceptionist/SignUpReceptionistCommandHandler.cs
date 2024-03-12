using BuildingBlocks.Abstractions.Commands;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Staff.Contracts;
using Staff.Core.Domain.Entities;
using Staff.Core.Domain.Exceptions;
using Staff.Core.Domain.Repositories;
using Staff.Core.Domain.ValueObjects;
using Staff.Core.Persistence;
using Staff.Core.Security;

namespace Staff.Core.Features.SignUpReceptionist;

internal class SignUpReceptionistCommandHandler : ICommandHandler<SignUpReceptionistCommand>
{
    private readonly IPasswordManager _passwordManager;
    private readonly StaffDbContext _staffDbContext;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ILogger<SignUpReceptionistCommandHandler> _logger;
    private readonly IBus _bus;

    public SignUpReceptionistCommandHandler(IPasswordManager passwordManager, StaffDbContext staffDbContext, IEmployeeRepository employeeRepository,
        ILogger<SignUpReceptionistCommandHandler> logger, IBus bus)
    {
        _passwordManager = passwordManager;
        _staffDbContext = staffDbContext;
        _employeeRepository = employeeRepository;
        _logger = logger;
        _bus = bus;
    }

    public async Task HandleAsync(SignUpReceptionistCommand command, CancellationToken cancellationToken = default)
    {
        if (await _staffDbContext.Employees.AnyAsync(e => e.Email == new Email(command.Email)))
            throw new EmailAlreadyExistsException(command.Email);

        var receptionist = Employee
            .CreateReceptionist(command.FirstName, command.LastName, command.Email, command.PhoneNumber, command.Password, _passwordManager);
        await _employeeRepository.AddEmployeeAsync(receptionist,cancellationToken);
        _logger.LogInformation("Create a receptionist with id: {id}", receptionist.Id);
        await _bus.Publish(new ReceptionistCreated(receptionist.Id, $"{receptionist.FirstName}, {receptionist.LastName}", receptionist.Email), cancellationToken);
    }
}
