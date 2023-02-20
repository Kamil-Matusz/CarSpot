using CarSpot.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSpot.Application.Commands
{
    public record SignUp(Guid UserId, string Email, string Username, string Password, string FullName, string Role) : ICommand;
}
