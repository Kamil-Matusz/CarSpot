using CarSpot.Application.Abstractions;
using CarSpot.Application.Commands;
using Microsoft.AspNetCore.Mvc;

namespace CarSpot.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ICommandHandler<SignUp> _signUpHandler;
        public UsersController(ICommandHandler<SignUp> signUpHandler)
        {
            _signUpHandler = signUpHandler;
        }

        [HttpPost]
        public async Task<ActionResult> Post(SignUp command)
        {
            command = command with { UserId = Guid.NewGuid() };
            await _signUpHandler.HandlerAsync(command);

            return NoContent();
        }
    }
}
