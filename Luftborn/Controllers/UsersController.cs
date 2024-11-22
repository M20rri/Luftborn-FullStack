namespace Luftborn.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _mediator.Send(new GetAllUsersQuery());
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var users = await _mediator.Send(new GetUserByIdQuery(id));
        return Ok(users);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
    {
        return Ok(await _mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command)
    {
        return Ok(await _mediator.Send(command));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        return Ok(await _mediator.Send(new DeleteUserCommand(id)));
    }
}
