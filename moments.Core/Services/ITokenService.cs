using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using moments.Core.Models;

namespace moments.Core.Services
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user, IList<string> roles, JwtSettings jwtSettings);
        Task<string> GenerateRefreshToken(Guid userId);
        Task<object> ReNewTokens(string refreshTokenValue, IList<string> roles, JwtSettings jwtSettings);
    }
}