using CarSpot.Api.Services;
using CarSpot.Application.Abstractions;
using CarSpot.Application.Exceptions;
using CarSpot.Application.Security;
using CarSpot.Core.Entities;
using CarSpot.Core.Repositories;
using CarSpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSpot.Application.Commands.Handlers
{
    internal sealed class SignUpHandler : ICommandHandler<SignUp>
    {
        private readonly IClock _clock;
        private readonly IPasswordManager _passwordManager;
        private readonly IUserRepository _userRepository;
        public SignUpHandler(IUserRepository userRepository,IPasswordManager passwordManager,IClock clock)
        {
            _userRepository = userRepository;
            _passwordManager = passwordManager;
            _clock = clock;
        }
        public async Task HandlerAsync(SignUp command)
        {
            var userId = new UserId(command.UserId);
            var email = new Email(command.Email);
            var username = new Username(command.Username);
            var password = new Password(command.Password);
            var fullName = new FullName(command.FullName);
            var role = string.IsNullOrWhiteSpace(command.Role) ? Role.User() : new Role(command.Role);

            if (await _userRepository.GetByEmailAsync(email) is not null)
            {
                throw new EmailAlreadyInUseException(email);
            }

            if (await _userRepository.GetByUsernameAsync(username) is not null)
            {
                throw new UsernameAlreadyInUseException(username);
            }

            var securedPassword = _passwordManager.Secure(password);
            var user = new User(userId, email, username, securedPassword, fullName, role, _clock.CurrentDate());
            await _userRepository.AddAsync(user);

        }
    }
}
