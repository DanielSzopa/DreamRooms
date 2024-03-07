using FluentValidation;

namespace Staff.Core.Features.SignUpReceptionist;
internal class SignUpReceptionistCommandValidator : AbstractValidator<SignUpReceptionistCommand>
{
	public SignUpReceptionistCommandValidator()
	{
		RuleFor(c => c.Email)
			.NotEmpty();

		RuleFor(c => c.FirstName)
			.NotEmpty();

		RuleFor(c => c.LastName)
			.NotEmpty();

		RuleFor(c => c.PhoneNumber)
			.NotEmpty();

		RuleFor(c => c.Password)
			.NotEmpty();

		RuleFor(c =>c.ConfirmedPassword)
			.NotEmpty()
			.Equal(C => C.Password);
	}
}
