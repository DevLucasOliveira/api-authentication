using Authentication.Domain.ValueObjects;

namespace Authentication.Domain.Entities
{
    public class User : Entity
    {
        public User() { }

        public User(string email, Password password)
        {
            Email = email;
            Password = password;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public Password Password { get; set; }
    }
}
