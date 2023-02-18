using CarSpot.Application.Abstractions;
using CarSpot.Application.Commands;
using CarSpot.Application.DTO;
using CarSpot.Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarSpot.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ICommandHandler<SignUp> _signUpHandler;
        private readonly IQueryHandler<GetUsers, IEnumerable<UserDto>> _getUsersHandler;
        private readonly IQueryHandler<GetUser, UserDto> _getUserHandler;
        public UsersController(ICommandHandler<SignUp> signUpHandler, IQueryHandler<GetUsers, IEnumerable<UserDto>> getUsersHandler, IQueryHandler<GetUser, UserDto> getUserHandler)
        {
            _signUpHandler = signUpHandler;
            _getUserHandler = getUserHandler;
            _getUsersHandler = getUsersHandler;
        }

        [HttpPost]
        public async Task<ActionResult> Post(SignUp command)
        {
            command = command with { UserId = Guid.NewGuid() };
            await _signUpHandler.HandlerAsync(command);

            return NoContent();
        }

        [HttpGet("{userId:guid}")]
        public async Task<ActionResult<UserDto>> Get(Guid userId)
        {
            var user = await _getUserHandler.HandleAsync(new GetUser { UserId = userId });
            if (user is null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> Get([FromQuery] GetUsers query)
        => Ok(await _getUsersHandler.HandleAsync(query));

        
    }
}
