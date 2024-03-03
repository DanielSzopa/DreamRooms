using BuildingBlocks.Abstractions.Commands;

namespace Staff.Core.Commands.Employees.SignUpReceptionist;

internal record SignUpReceptionistCommand(string FirstName, string LastName, string Email, string PhoneNumber, string Password, string ConfirmedPassword) : ICommand;