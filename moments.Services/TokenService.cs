using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using moments.Core;
using moments.Core.Models;
using moments.Core.Services;

namespace moments.Services
{
    public class TokenService : ITokenService
    {
        private IUnitOfWork _unitOfWork;

        public TokenService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public string GenerateAccessToken(User user, IList<string> roles, JwtSettings jwtSettings)
        {
            var claims = new List<Claim>
            {
                // datos que va a contener el token
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()), // id de usuario (como subject del jwt)
                new Claim(ClaimTypes.Name, user.UserName), // nombre de usuario
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // id del token
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) // id de usuario
            };

            IEnumerable<Claim> roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r)); // creo un claim para cada rol al que pertenezca el usuario y lo agrego a la lista
            claims.AddRange(roleClaims); // agrego todos los roles al token

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtSettings.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings.ExpirationInHours)); // tiempo de expiración (valor extraído de appsettings)

            var token = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims,
                expires: expires,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> GenerateRefreshToken(Guid userId)
        {
            var refreshToken = new RefreshToken()
            {
                IsActive = true,
                ExpirationDate = DateTime.UtcNow.AddMinutes(5),
                Value = Guid.NewGuid().ToString("N"),
                IdUser = userId,
                IsUsed = false
            };

            if(await _unitOfWork.RefreshToken.AddAsync(refreshToken))
                await _unitOfWork.CommitAsync();
            else
                throw new SystemException("Hubo un problema al crear el refresh token.");

            return refreshToken.Value;
        }

        public async Task<object> ReNewTokens(string refreshTokenValue, IList<string> roles, JwtSettings jwtSettings)
        {
            var refreshToken = await _unitOfWork.RefreshToken.SingleOrDefaultAsync(rt => rt.Value == refreshTokenValue);

            if(refreshToken is null || !refreshToken.IsActive || refreshToken.ExpirationDate >= DateTime.UtcNow)
                throw new ArgumentException("Token de refresco no válido.");

            if(refreshToken.IsUsed)
            {
                var userRefreshTokens = await _unitOfWork.RefreshToken.SearchAsync(rt => rt.IsActive && !rt.IsUsed && rt.IdUser == refreshToken.IdUser);

                foreach(var userRefreshToken in userRefreshTokens.ToList())
                {
                    userRefreshToken.IsActive = false;
                    userRefreshToken.IsUsed = true;
                }

                await _unitOfWork.CommitAsync();
                throw new ArgumentException($"El token de refresco ({refreshToken.Value} ya fue utilizado. Por seguridad, se invalidarán todos los tokens de refresco del usuario.)");
            }

            refreshToken.IsUsed = true;

            User user = await _unitOfWork.Users.SingleOrDefaultAsync(u => u.Id == refreshToken.IdUser);

            if(user is null)
                throw new ArgumentException("El usuario al que hace referencia el token de refresco no existe.");
            
            var newAccessToken = this.GenerateAccessToken(user,roles,jwtSettings);
            var newRefreshToken = await this.GenerateRefreshToken(user.Id);

            return new { AccessToken = newAccessToken, RefreshToken = newRefreshToken };
        }
    }
}