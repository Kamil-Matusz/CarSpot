using CarSpot.Application.DTO;
using CarSpot.Application.Security;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSpot.Infrastructure.Auth
{
    internal sealed class HttpContextTokenStorage : ITokenStorage
    {
        private const string TokenKey = "jwt";
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HttpContextTokenStorage(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public JwtDto GetJwt()
        {
            if(_httpContextAccessor is null)
            {
                return null;
            }

            if(_httpContextAccessor.HttpContext.Items.TryGetValue(TokenKey,out var jwt))
            {
                return jwt as JwtDto;
            }
            else
            {
                return null;
            }
        }

        public void Set(JwtDto jwt) => _httpContextAccessor.HttpContext?.Items.TryAdd(TokenKey, jwt);

    }
}
