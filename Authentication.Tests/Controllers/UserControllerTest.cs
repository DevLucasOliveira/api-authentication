using Authentication.Api.Controllers;
using Authentication.Domain.Commands;
using Authentication.Domain.Services.Interfaces;
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
        private readonly Mock<IMediator> _mockedMediator;
        private readonly Mock<IUserService> _mockedUserService;

        public UserControllerTest()
        {
            _mockedMediator = new Mock<IMediator>();
            _mockedUserService = new Mock<IUserService>();
            _userController = new UserController();
        }


        [Fact]
        public void ValidateUser_ShouldReturnOk()
        {
            // Arrange
            var command = new ValidateUserCommand { Email = "usuariofake@teste.com", Password = "HEJDKCcxsjeU@Uab82" };

            _mockedMediator.Setup(x => x.Send(It.IsAny<ValidateUserCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new RequestResult());

            // Act
            var response = _userController.ValidateUser(command, _mockedMediator.Object).Result;
            var objectResult = response as OkObjectResult;

            // Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal((int)HttpStatusCode.OK, objectResult.StatusCode);
        }

        [Fact]
        public void ValidatePassword_ShouldReturnOk()
        {
            // Arrange
            var command = new ValidatePasswordCommand { Password = "HEJDKCcxsjeU@Uab82" };

            _mockedMediator.Setup(x => x.Send(It.IsAny<ValidatePasswordCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new RequestResult());

            // Act
            var response = _userController.ValidatePassword(command, _mockedMediator.Object).Result;
            var objectResult = response as OkObjectResult;

            // Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal((int)HttpStatusCode.OK, objectResult.StatusCode);
        }

        [Fact]
        public void GeneratePassword_ShouldReturnOk()
        {
            // Arrange
            _mockedUserService.Setup(x => x.GeneratePassword()).Returns(new RequestResult());

            // Act
            var response = _userController.GeneratePassword(_mockedUserService.Object);
            var objectResult = response as OkObjectResult;

            // Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal((int)HttpStatusCode.OK, objectResult.StatusCode);
        }


    }
}
