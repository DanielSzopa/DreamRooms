using BuildingBlocks.Domain.Entities;
using Staff.Core.Domain.Events;
using Staff.Core.Domain.ValueObjects;
using Staff.Core.Security;

namespace Staff.Core.Domain.Entities;

internal sealed class Employee : Entity, IAgregateRoot
{
    internal EmployeeId Id { get; private set; }
    internal FirstName FirstName { get; private set; }
    internal LastName LastName { get; private set; }
    internal Email Email { get; private set; }
    internal PasswordHash PasswordHash { get; private set; }
    internal PhoneNumber PhoneNumber { get; private set; }
    internal Role Role { get; private set; }

    private Employee(EmployeeId id, FirstName firstName, LastName lastName, Email email, PhoneNumber phoneNumber, Role role, PasswordHash passwordHash)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Role = role;
        PasswordHash = passwordHash;

        AddParticularDomainEvent(Id, FirstName, LastName, Email, Role);
    }

    internal static Employee CreateReceptionist(FirstName firstName, LastName lastName, Email email, PhoneNumber phoneNumber, Password password, IPasswordManager passwordManager)
    {
        return CreateEmployee(firstName, lastName, email, phoneNumber, Role.Receptionist, password, passwordManager);
    }

    internal static Employee CreateRoomServicer(FirstName firstName, LastName lastName, Email email, PhoneNumber phoneNumber, Password password, IPasswordManager passwordManager)
    {
        return CreateEmployee(firstName, lastName, email, phoneNumber, Role.RoomServicer, password, passwordManager);
    }

    private static Employee CreateEmployee(FirstName firstName, LastName lastName, Email email, PhoneNumber phoneNumber, Role role, Password password, IPasswordManager passwordManager)
    {
        var passwordHash = passwordManager.HashPassword(password);
        return new(Guid.NewGuid(), firstName, lastName, email, phoneNumber, role, passwordHash);
    }

    private void AddParticularDomainEvent(Guid Id, string FirstName, string LastName, string Email, Role role)
    {
        if (role == Role.Receptionist)
        {
            this.AddDomainEvent(new ReceptionistCreated(Id, $"{FirstName} {LastName}", Email));
        }
        else if(role == Role.RoomServicer)
        {
            //later
        }
    }
}
