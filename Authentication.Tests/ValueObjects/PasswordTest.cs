using Authentication.Domain.ValueObjects;
using Xunit;

namespace Authentication.Tests.ValueObjects
{
    public class PasswordTest
    {
        private readonly Password _password;

        public PasswordTest()
        {
            _password = new Password();
        }

        [Fact]
        public void GeneratePassword_ShouldReturnPassword()
        {
            var result = _password.GeneratePassword();

            Assert.True(Password.ValidatePassword(result));
        }

        [Fact]
        public void ValidatePassword_WhenPasswordIsLowerThan15characters_ShouldReturnFalse()
        {
            var password = "HEJDKCcsjeU@Ua";

            var result = Password.ValidatePassword(password);

            Assert.False(result);
        }

        [Fact]
        public void ValidatePassword_WhenPasswordHasNotUpperChar_ShouldReturnFalse()
        {
            var password = "hejdkctsjeu@uab82";

            var result = Password.ValidatePassword(password);

            Assert.False(result);
        }

        [Fact]
        public void ValidatePassword_WhenPasswordHasNotLowerChar_ShouldReturnFalse()
        {
            var password = "HEJDCXJED@UAB82";

            var result = Password.ValidatePassword(password);

            Assert.False(result);
        }


        [Fact]
        public void ValidatePassword_WhenPasswordHasNotSymbols_ShouldReturnFalse()
        {
            var password = "HEJDKCcxsjeUFUab82";

            var result = Password.ValidatePassword(password);

            Assert.False(result);
        }

        [Fact]
        public void ValidatePassword_WhenPasswordHasRepeatedChar_ShouldReturnFalse()
        {
            var password = "HEJDKCccxsjeU@Uab82";

            var result = Password.ValidatePassword(password);

            Assert.False(result);
        }

        [Fact]
        public void ValidatePassword_WhenPasswordIsValid_ShouldReturnTrue()
        {
            var password = "HEJDKCcxsjeU@Uab82";

            var result = Password.ValidatePassword(password);

            Assert.True(result);
        }

        [Fact]
        public void VerifyPassword_WhenPasswordIsTheSame_ShouldReturnTrue()
        {
            var passwordCommand = "HEJDKCcxsjeU@Uab82";
            var passwordUser = "HEJDKCcxsjeU@Uab82";

            var result = Password.VerifyPassword(passwordCommand, passwordUser);

            Assert.True(result);
        }


        [Fact]
        public void VerifyPassword_WhenPasswordIsNotTheSame_ShouldReturnFalse()
        {
            var passwordCommand = "HEJDKCcxsjeU@Uab82";
            var passwordUser = "HEJDKCcxsjeU@Uab23";

            var result = Password.VerifyPassword(passwordCommand, passwordUser);

            Assert.False(result);
        }



    }
}
