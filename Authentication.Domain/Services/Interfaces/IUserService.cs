using Authentication.Domain.Commands;

namespace Authentication.Domain.Services.Interfaces
{
    public interface IUserService
    {
        RequestResult GeneratePassword();
    }
}
