using Authentication.Domain.Commands;
using Authentication.Domain.DTOs;
using Authentication.Domain.Entities;
using Authentication.Domain.Services.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Authentication.Domain.Handlers
{
    public class UserHandler : IRequestHandler<ValidateUserCommand, RequestResult>
    {
        private readonly ITokenService _tokenService;

        public UserHandler(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public async Task<RequestResult> Handle(ValidateUserCommand command, CancellationToken cancellationToken)
        {
            command.Validate();
            if (command.Invalid)
                return new RequestResult(false, "Ocorreu um erro", command.Notifications.ToList());

            var user = new User();
            var token = _tokenService.GenerateToken(user);

            var userDTO = new UserDTO
            {
                Email = user.Email,
                IsAuthenticated = true,
                Token = token.Token,
                TokenExpireIn = token.ExpireIn
            };

            return new RequestResult(true, "Token gerado com sucesso", userDTO);
        }




    }
}
