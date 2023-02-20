using CarSpot.Application.Abstractions;
using CarSpot.Application.Commands;
using CarSpot.Application.DTO;
using CarSpot.Application.Queries;
using CarSpot.Application.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarSpot.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ICommandHandler<SignUp> _signUpHandler;
        private readonly ICommandHandler<SignIn> _signInHandler;
        private readonly ITokenStorage _tokenStorage;

        public UsersController(ICommandHandler<SignUp> signUpHandler,ICommandHandler<SignIn> signInHandler,ITokenStorage tokenStorage)
        {
            _signUpHandler = signUpHandler;
            _signInHandler = signInHandler;
            _tokenStorage = tokenStorage;
        }

        [HttpPost]
        public async Task<ActionResult> SignUp(SignUp command)
        {
            command = command with { UserId = Guid.NewGuid() };
            await _signUpHandler.HandlerAsync(command);

            return NoContent();
        }
        
        [HttpPost("sign-in")]
        public async Task<ActionResult<JwtDto>> SignIn(SignIn command)
        {
            await _signInHandler.HandlerAsync(command);
            var jwt = _tokenStorage.GetJwt();
            return Ok(jwt);
        }


    }
}
