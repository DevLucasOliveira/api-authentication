using Flunt.Notifications;
using Flunt.Validations;
using MediatR;

namespace Authentication.Domain.Commands
{
    public class ValidateUserCommand : Notifiable, IValidatable, IRequest<RequestResult>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
                           .Requires()
                           .IsEmail(Email, "Email", "Email inválido")
                           .IsNotNullOrEmpty(Password, "Password", "A senha não pode ser nula"));
        }
    }
}
