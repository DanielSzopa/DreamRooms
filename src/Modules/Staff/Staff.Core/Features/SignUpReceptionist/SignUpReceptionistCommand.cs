using BuildingBlocks.Abstractions.Commands;

namespace Staff.Core.Features.SignUpReceptionist;

internal record SignUpReceptionistCommand(string FirstName, string LastName, string Email, string PhoneNumber, string Password, string ConfirmedPassword) : ICommand;