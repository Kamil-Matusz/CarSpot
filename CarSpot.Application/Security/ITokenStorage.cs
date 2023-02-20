using CarSpot.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSpot.Application.Security
{
    public interface ITokenStorage
    {
        void Set(JwtDto jwt);
        JwtDto GetJwt();
    }
}
