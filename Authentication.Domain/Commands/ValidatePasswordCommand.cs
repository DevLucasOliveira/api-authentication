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
            throw new System.NotImplementedException();
        }
    }
}
