using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Authentication.Domain.ValueObjects
{
    public class Password
    {
        public Password() { }

        public Password(string passwordUser)
        {
            PasswordUser = passwordUser;
        }

        public string PasswordUser { get; set; }

        public string GeneratePassword()
        {
            const string charactersValid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#_-";

            StringBuilder password;
            var random = new Random();

            do
            {
                var passwordLength = new Random().Next(15, 25);
                password = new StringBuilder();

                while (0 < passwordLength--)
                {
                    password.Append(charactersValid[random.Next(charactersValid.Length)]);
                }

            } while (!ValidatePassword(password.ToString()));

            return password.ToString();
        }

        public static bool VerifyPassword(string passwordCommand, string passwordUser)
        {
            return passwordUser.Equals(passwordCommand);
        }

        public static bool ValidatePassword(string password)
        {
            var hasMinimum15Chars = new Regex(@".{15,}");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#_-]");
            var hasRepeatedChar = PasswordHasRepeatedChar(password);

            return hasMinimum15Chars.IsMatch(password) &&
                hasUpperChar.IsMatch(password) &&
                hasLowerChar.IsMatch(password) &&
                hasSymbols.IsMatch(password) &&
                !hasRepeatedChar;
        }

        private static bool PasswordHasRepeatedChar(string password)
        {
            var chars = password.ToCharArray();
            for (int i = 0; i < chars.Length - 1; i++)
            {
                if (chars[i] == chars[i + 1])
                    return true;
            }

            return false;
        }

    }
}
