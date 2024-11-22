namespace Luftborn.CQRS.Commands;

public sealed record CreateUserCommand(UserDto User) : IRequest<int>;
public sealed record CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<UserDto> _validator;

    public CreateUserCommandHandler(IUserRepository userRepository, IValidator<UserDto> validator)
    {
        _userRepository = userRepository;
        _validator = validator;
    }

    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var personValidator = await _validator.ValidateAsync(request.User);
        if (personValidator.Errors.Any())
        {
            var Errors = personValidator.Errors.Select(x => x.ErrorMessage).ToList();
            throw new Exceptions.ValidationException(Errors, (int)HttpStatusCode.BadRequest);
        }

        var user = new User
        {
            FirstName = request.User.FirstName,
            LastName = request.User.LastName,
            Email = request.User.Email
        };

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        return user.Id;
    }
}