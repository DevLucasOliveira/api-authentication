using Authentication.Domain.DTOs;
using Authentication.Domain.Entities;
using Authentication.Domain.Services.Interfaces;
using Authentication.Domain.Utils;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Authentication.Domain.Services
{
    public class TokenService : ITokenService
    {
        public TokenDTO GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);

            var claims = new List<Claim>()
            {
                new Claim("user_id", user.Id.ToString()),
                new Claim("user_email", user.Email),
            };

            var expireIn = DateTime.UtcNow.AddMinutes(5);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expireIn,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenWrite = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(tokenWrite);

            return new TokenDTO(token, GetBrazilTimezone(expireIn));
        }

        private DateTime GetBrazilTimezone(DateTime dateUtc)
        {
            var brasiliaTime = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(dateUtc, brasiliaTime);
        }


    }
}
