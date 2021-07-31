using System;

namespace Authentication.Domain.DTOs
{
    public class UserDTO
    {
        public UserDTO(string email, bool isAuthenticated)
        {
            Email = email;
            IsAuthenticated = isAuthenticated;
        }

        public UserDTO(string email, bool isAuthenticated, string token, DateTime tokenExpireIn)
        {
            Email = email;
            IsAuthenticated = isAuthenticated;
            Token = token;
            TokenExpireIn = tokenExpireIn;
        }

        public string Email { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpireIn { get; set; }
    }
}
