using Microsoft.IdentityModel.Tokens;
using SyncingSyllabi.Data.Settings;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SyncingSyllabi.Common.Tools.Utilities
{
    public class TokenUtility
    {
        public static string GenerateAccessToken(long userId, string email, string fullname)
        {
            var authSettings = ConfigurationUtility.GetConfig<AuthSettings>("AuthSettings");

            DateTime? expiration = null;

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Name, fullname),
                new Claim(ClaimTypes.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, fullname)

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.Key));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
              authSettings.Issuer,
              authSettings.Audience,
              claims,
              //expires: authSettings.ExpirationInMinutes == null ? expiration : DateTime.Now.AddMinutes(Convert.ToInt32(authSettings.ExpirationInMinutes)),
              signingCredentials: credential);

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
