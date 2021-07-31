using System;

namespace Authentication.Domain.Entities
{
    public class User : Entity
    {
        public User() {}

        public string Email { get; set; }
        public string Password { get; set; }
    }
}
