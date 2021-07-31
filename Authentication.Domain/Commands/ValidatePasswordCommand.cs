using Flunt.Notifications;
using Flunt.Validations;
using MediatR;

namespace Authentication.Domain.Commands
{
    public class ValidatePasswordCommand : Notifiable, IValidatable, IRequest<RequestResult>
    {
        public string Password { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
                           .Requires()
                           .IsNotNullOrEmpty(Password, "Password", "A senha não pode ser nula"));
        }
    }
}
