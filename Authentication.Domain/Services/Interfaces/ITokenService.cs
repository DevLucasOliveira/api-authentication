using Authentication.Domain.DTOs;
using Authentication.Domain.Entities;

namespace Authentication.Domain.Services.Interfaces
{
    public interface ITokenService
    {
        TokenDTO GenerateToken(User user);
    }
}
