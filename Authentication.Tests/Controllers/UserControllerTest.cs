using Authentication.Api.Controllers;
using Authentication.Domain.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using System.Threading;
using Xunit;

namespace Authentication.Tests.Controllers
{
    public class UserControllerTest
    {
        private readonly UserController _userController;
        private readonly Mock<IMediator> _mediator;

        public UserControllerTest()
        {
            _mediator = new Mock<IMediator>();
            _userController = new UserController();
        }


        [Fact]
        public void ValidateUser_ShouldReturnOk()
        {
            // Arrange
            var command = new ValidateUserCommand { Email = "usuario@teste.com", Password = "124532" };

            _mediator.Setup(x => x.Send(It.IsAny<ValidateUserCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new RequestResult());

            // Act
            var response = _userController.ValidateUser(command, _mediator.Object).Result;
            var objectResult = response as OkObjectResult;
            
            // Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal((int)HttpStatusCode.OK, objectResult.StatusCode);
        }



    }
}
