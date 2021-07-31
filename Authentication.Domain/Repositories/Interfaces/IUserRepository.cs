using Authentication.Domain.Entities;

namespace Authentication.Domain.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User GetUser(string email);
    }
}
