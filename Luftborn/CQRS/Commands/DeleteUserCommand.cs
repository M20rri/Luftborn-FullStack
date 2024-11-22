namespace Luftborn.CQRS.Commands;

public sealed record DeleteUserCommand(int UserId) : IRequest<int>;

public sealed record DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, int>
{
    private readonly IUserRepository _userRepository;
    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<int> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user == null)
        {
            throw new Exceptions.ValidationException($"User with ID {request.UserId} not found.", (int)HttpStatusCode.BadRequest);
        }

        await _userRepository.DeleteAsync(request.UserId);
        await _userRepository.SaveChangesAsync();
        return request.UserId;
    }
}
