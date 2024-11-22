namespace Luftborn.Validators;

public class UserDtoValidator : AbstractValidator<UserDto>
{
    public UserDtoValidator()
    {
        RuleFor(user => user.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .Length(2, 50).WithMessage("First name must be between 2 and 50 characters.");

        RuleFor(user => user.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .Length(2, 50).WithMessage("Last name must be between 2 and 50 characters.");

        RuleFor(user => user.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email is required.")
            .Length(5, 100).WithMessage("Email must be between 5 and 100 characters.");
    }
}
