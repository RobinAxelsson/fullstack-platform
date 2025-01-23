using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using TenStar.UserContext.App.Exceptions;

namespace TenStar.UserContext.App.Validators
{
    internal static class UserValidator
    {
        public static IReadOnlyCollection<UserValidationError> ValidateUserInput(string name, string email, string password, string username)
        {
            var errors = new List<UserValidationError>();
            if (string.IsNullOrWhiteSpace(name) || name.Length > 100)
                errors.Add(UserValidationError.FullName);

            if (string.IsNullOrWhiteSpace(username) || username.Length > 100)
                errors.Add(UserValidationError.Username);

            if (string.IsNullOrWhiteSpace(email) || !new EmailAddressAttribute().IsValid(email))
                errors.Add(UserValidationError.Email);

            if (string.IsNullOrWhiteSpace(password) || !IsValidPassword(password))
                errors.Add(UserValidationError.Pasword);

            return errors;
        }

        private static bool IsValidPassword(string password)
        {
            var hasUpperCase = new Regex(@"[A-Z]+");
            var hasLowerCase = new Regex(@"[a-z]+");
            var hasDigit = new Regex(@"\d+");
            var hasSpecialChar = new Regex(@"[\W_]+");

            return password.Length >= 8 && hasUpperCase.IsMatch(password) && hasLowerCase.IsMatch(password) && hasDigit.IsMatch(password) && hasSpecialChar.IsMatch(password);
        }
    }
}
