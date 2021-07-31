using Authentication.Domain.Entities;
using Authentication.Domain.Repositories.Interfaces;
using Authentication.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace Authentication.Domain.Repositories
{
    public class UserRepository : IUserRepository
    {

        // Método fake apenas para funcionamento do Handler
        public User GetUser(string email)
        {
            var users = new List<User>()
            {
                new User("usuariofake@teste.com", new Password("HEJDKCcxsjeU@Uab82")),
                new User("usuariofake2@teste.com", new Password("yv1NV3afZgT!fWXWOTYHI")),
                new User("usuariofake3@teste.com", new Password("8#1yv1NV3cxsjeTY389HI"))
            };

            return users.FirstOrDefault(x => x.Email.Equals(email));
        }

    }
}
