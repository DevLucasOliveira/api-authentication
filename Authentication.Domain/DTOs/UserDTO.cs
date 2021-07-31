using System;

namespace Authentication.Domain.DTOs
{
    public class UserDTO
    {
        public string Email { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpireIn { get; set; }
    }
}
