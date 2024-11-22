namespace Luftborn.CQRS.Commands;

public sealed record UpdateUserCommand(UserDto User) : IRequest<int>;

public sealed record UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, int>
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<UserDto> _validator;
    public UpdateUserCommandHandler(IUserRepository userRepository, IValidator<UserDto> validator)
    {
        _userRepository = userRepository;
        _validator = validator;
    }

    public async Task<int> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var personValidator = await _validator.ValidateAsync(request.User);
        if (personValidator.Errors.Any())
        {
            var Errors = personValidator.Errors.Select(x => x.ErrorMessage).ToList();
            throw new Exceptions.ValidationException(Errors, (int)HttpStatusCode.BadRequest);
        }

        var user = await _userRepository.GetByIdAsync(request.User.Id);
        if (user == null)
        {
            throw new Exceptions.ValidationException($"User with ID {request.User.Id} not found.", (int)HttpStatusCode.BadRequest);
        }


        user.FirstName = request.User.FirstName;
        user.LastName = request.User.LastName;
        user.Email = request.User.Email;

        await _userRepository.UpdateAsync(user);
        await _userRepository.SaveChangesAsync();

        return request.User.Id;
    }

}
