using Authentication.Domain.Commands;
using Authentication.Domain.DTOs;
using Authentication.Domain.Repositories.Interfaces;
using Authentication.Domain.Services.Interfaces;
using Authentication.Domain.ValueObjects;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Authentication.Domain.Handlers
{
    public class UserHandler : IUserService,
        IRequestHandler<ValidateUserCommand, RequestResult>,
        IRequestHandler<ValidatePasswordCommand, RequestResult>
    {
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;

        public UserHandler(ITokenService tokenService, IUserRepository userRepository)
        {
            _tokenService = tokenService;
            _userRepository = userRepository;
        }

        public async Task<RequestResult> Handle(ValidateUserCommand command, CancellationToken cancellationToken)
        {
            command.Validate();
            if (command.Invalid)
                return new RequestResult(false, "Ocorreu um erro", new UserDTO(command.Email, false));

            var user = _userRepository.GetUser(command.Email);
            if (user == null)
                return new RequestResult(false, "Email ou senha inválida", new UserDTO(command.Email, false));

            var passwordValid = Password.VerifyPassword(command.Password, user.Password.PasswordUser);
            if (!passwordValid)
                return new RequestResult(false, "Email ou senha inválida", new UserDTO(command.Email, false));

            var token = _tokenService.GenerateToken(user);
            var userDTO = new UserDTO(user.Email, true, token.Token, token.ExpireIn);

            return new RequestResult(true, "Token gerado com sucesso", userDTO);
        }

        public async Task<RequestResult> Handle(ValidatePasswordCommand command, CancellationToken cancellationToken)
        {
            command.Validate();
            if (command.Invalid)
                return new RequestResult(false, "Ocorreu um erro", new PasswordDTO(command.Password, false));

            var passwordDTO = new PasswordDTO
            {
                Password = command.Password,
                IsValid = Password.ValidatePassword(command.Password)
            };

            return new RequestResult(true, "Senha verificada com sucesso", passwordDTO);
        }

        public RequestResult GeneratePassword()
        {
            return new RequestResult(true, "Senha gerada com sucesso", new Password().GeneratePassword());
        }

    }
}
