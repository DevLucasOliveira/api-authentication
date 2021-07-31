using System;

namespace Authentication.Domain.DTOs
{
    public class TokenDTO
    {
        public TokenDTO(string token, DateTime expireIn)
        {
            Token = token;
            ExpireIn = expireIn;
        }

        public string Token { get; set; }
        public DateTime ExpireIn { get; set; }
    }
}
