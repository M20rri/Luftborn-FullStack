namespace Luftborn.CQRS.Queries;

public sealed record GetUserByIdQuery(int UserId) : IRequest<UserDto>;
public sealed record GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        return _mapper.Map<UserDto>(user);
    }
}