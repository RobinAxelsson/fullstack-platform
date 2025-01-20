using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Text;
using TenStar.App.Exceptions;

namespace TenStar.App.Entities
{
    public sealed class User
    {
        [Obsolete("This constructor is EF only to maintain validation", error: true)]
        public User() { }
        public int DbId { get; set; }
        public Guid TsId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }

        internal User(string name, string email, string password, string username)
        {
            var errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(name) || name.Length > 100)
                errors.AppendLine("Full name is required and must be less than 100 characters.");

            if (string.IsNullOrWhiteSpace(username) || username.Length > 100)
                errors.AppendLine("Username is required and must be less than 100 characters.");

            if (string.IsNullOrWhiteSpace(email) || !new EmailAddressAttribute().IsValid(email))
                errors.AppendLine("A valid email address is required.");

            if (string.IsNullOrWhiteSpace(password) || !IsValidPassword(password))
                errors.AppendLine("Password must be at least 8 characters long and contain at least one upper case letter, one lower case letter, one digit, and one special character.");

            if (errors.Length > 0)
                throw new UserInvalidException(errors.ToString());

            TsId = Guid.NewGuid();
            Name = name;
            Email = email;
            Password = password;
            Username = username;
        }

        private bool IsValidPassword(string password)
        {
            var hasUpperCase = new Regex(@"[A-Z]+");
            var hasLowerCase = new Regex(@"[a-z]+");
            var hasDigit = new Regex(@"\d+");
            var hasSpecialChar = new Regex(@"[\W_]+");

            return password.Length >= 8 && hasUpperCase.IsMatch(password) && hasLowerCase.IsMatch(password) && hasDigit.IsMatch(password) && hasSpecialChar.IsMatch(password);
        }
    }
}
