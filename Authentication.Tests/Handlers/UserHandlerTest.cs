using Authentication.Domain.Commands;
using Authentication.Domain.DTOs;
using Authentication.Domain.Entities;
using Authentication.Domain.Handlers;
using Authentication.Domain.Repositories.Interfaces;
using Authentication.Domain.Services.Interfaces;
using Authentication.Domain.ValueObjects;
using Moq;
using System;
using System.Threading;
using Xunit;

namespace Authentication.Tests.Handlers
{
    public class UserHandlerTest
    {
        private readonly UserHandler _userHandler;
        private readonly Mock<ITokenService> _mockedTokenService;
        private readonly Mock<IUserRepository> _mockedUserRepository;

        public UserHandlerTest()
        {
            _mockedTokenService = new Mock<ITokenService>();
            _mockedUserRepository = new Mock<IUserRepository>();
            _userHandler = new UserHandler(_mockedTokenService.Object, _mockedUserRepository.Object);
        }


        [Fact]
        public void ValidateUserCommand_WhenCommandIsInvalid_ShouldReturnError()
        {
            // Arrange
            var command = new ValidateUserCommand { Email = "usuario.com", Password = "123456" };

            // Act
            var result = _userHandler.Handle(command, It.IsAny<CancellationToken>()).Result;

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result.Message);
            Assert.NotNull(result.Data);
            Assert.IsType<UserDTO>(result.Data);
        }


        [Fact]
        public void ValidateUserCommand_WhenUserIsNotRegistered_ShouldReturnError()
        {
            // Arrange
            var command = new ValidateUserCommand { Email = "usuario@email.com", Password = "123456" };

            _mockedUserRepository.Setup(x => x.GetUser(It.IsAny<string>()));

            // Act
            var result = _userHandler.Handle(command, It.IsAny<CancellationToken>()).Result;

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result.Message);
            Assert.NotNull(result.Data);
            Assert.IsType<UserDTO>(result.Data);

            _mockedUserRepository.Verify(x => x.GetUser(It.IsAny<string>()), Times.Once);
        }


        [Fact]
        public void ValidateUserCommand_WhenPasswordIsNotTheSame_ShouldReturnError()
        {
            // Arrange
            var command = new ValidateUserCommand { Email = "usuario@email.com", Password = "123456" };
            var user = new User(command.Email, new Password("12345533367"));

            _mockedUserRepository.Setup(x => x.GetUser(It.IsAny<string>())).Returns(user);

            // Act
            var result = _userHandler.Handle(command, It.IsAny<CancellationToken>()).Result;

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result.Message);
            Assert.NotNull(result.Data);
            Assert.IsType<UserDTO>(result.Data);

            _mockedUserRepository.Verify(x => x.GetUser(It.IsAny<string>()), Times.Once);
        }


        [Fact]
        public void ValidateUserCommandCommand_ShouldReturnSuccess()
        {
            // Arrange
            var command = new ValidateUserCommand { Email = "usuario@email.com", Password = "123456" };
            var user = new User(command.Email, new Password("123456"));
            var token = new TokenDTO("EFNIU3r//TokenTest", DateTime.Now.AddMinutes(5));

            _mockedUserRepository.Setup(x => x.GetUser(It.IsAny<string>())).Returns(user);
            _mockedTokenService.Setup(x => x.GenerateToken(It.IsAny<User>())).Returns(token);

            // Act
            var result = _userHandler.Handle(command, It.IsAny<CancellationToken>()).Result;

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result.Message);
            Assert.NotNull(result.Data);
            Assert.IsType<UserDTO>(result.Data);

            _mockedUserRepository.Verify(x => x.GetUser(It.IsAny<string>()), Times.Once);
            _mockedTokenService.Verify(x => x.GenerateToken(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public void ValidatePasswordCommand_WhenCommandIsInvalid_ShouldReturnError()
        {
            // Arrange
            var command = new ValidatePasswordCommand { Password = "" };

            // Act
            var result = _userHandler.Handle(command, It.IsAny<CancellationToken>()).Result;
            var content = result.Data as PasswordDTO;

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result.Message);
            Assert.NotNull(result.Data);
            Assert.IsType<PasswordDTO>(result.Data);
            Assert.False(content.IsValid);
        }

        [Fact]
        public void ValidatePasswordCommand_WhenPasswordIsInvalid_ShouldReturnError()
        {
            // Arrange
            var command = new ValidatePasswordCommand { Password = "yv1NVfWXWOTYHI" };

            // Act
            var result = _userHandler.Handle(command, It.IsAny<CancellationToken>()).Result;
            var content = result.Data as PasswordDTO;

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result.Message);
            Assert.NotNull(result.Data);
            Assert.IsType<PasswordDTO>(result.Data);
            Assert.False(content.IsValid);
            Assert.NotNull(content.Password);
        }

        [Fact]
        public void ValidatePasswordCommand_WhenPasswordIsValid_ShouldReturnSuccess()
        {
            // Arrange
            var command = new ValidatePasswordCommand { Password = "yv1NV3afZgT!fWXWOTYHI" };

            // Act
            var result = _userHandler.Handle(command, It.IsAny<CancellationToken>()).Result;
            var content = result.Data as PasswordDTO;

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result.Message);
            Assert.NotNull(result.Data);
            Assert.IsType<PasswordDTO>(result.Data);
            Assert.True(content.IsValid);
            Assert.NotNull(content.Password);
        }

        [Fact]
        public void GeneratePassword_ShouldReturnOk()
        {
            // Act
            var result = _userHandler.GeneratePassword();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result.Message);
            Assert.NotNull(result.Data);
            Assert.IsType<string>(result.Data);
        }



    }
}
