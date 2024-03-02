using BuildingBlocks.Domain.Entities;
using Staff.Core.Domain.ValueObjects;

namespace Staff.Core.Domain.Entities;

public sealed class Employee : Entity, IAgregateRoot
{
    public EmployeeId Id { get; private set; }
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public Email Email { get; private set; }
    public string Password { get; private set; }
    public string HashPassword { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public Role Role { get; private set; }

    private Employee(EmployeeId id, FirstName firstName, LastName lastName, Email email, PhoneNumber phoneNumber, Role role, string hashPassword)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Role = role;
        HashPassword = hashPassword;
    }

    public static Employee CreateReceptionist(FirstName firstName, LastName lastName, Email email, PhoneNumber phoneNumber, string password)
    {
        return new(Guid.NewGuid(), firstName, lastName, email, phoneNumber, Role.Receptionist);
    }

    public static Employee CreateRoomServicer(FirstName firstName, LastName lastName, Email email, PhoneNumber phoneNumber, string password)
    {
        var test = CreateEmployee(Guid.NewGuid(), firstName, lastName, email, phoneNumber, Role.RoomServicer, password);
        return new(Guid.NewGuid(), firstName, lastName, email, phoneNumber, Role.RoomServicer);
    }

    private static Employee CreateEmployee(EmployeeId id, FirstName firstName, LastName lastName, Email email, PhoneNumber phoneNumber, Role role, string password)
    {
        //Need add HashPassword
        Password = "HashPassword";
    }
}
