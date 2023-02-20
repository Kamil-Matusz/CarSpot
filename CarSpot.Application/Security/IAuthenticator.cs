using CarSpot.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSpot.Application.Security
{
    public interface IAuthenticator
    {
        JwtDto CreateToken(Guid userId, string role);
    }
}
