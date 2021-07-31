namespace Authentication.Domain.DTOs
{
    public class PasswordDTO
    {
        public PasswordDTO() { }

        public PasswordDTO(string password, bool isValid)
        {
            Password = password;
            IsValid = isValid;
        }

        public string Password { get; set; }
        public bool IsValid { get; set; }
    }
}
