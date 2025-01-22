using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace TenStar.App.Validators
{
    internal static class UserValidator
    {
        public static IReadOnlyCollection<UserValidatorError> ValidateUserInput(string name, string email, string password, string username)
        {
            var errors = new List<UserValidatorError>();
            if (string.IsNullOrWhiteSpace(name) || name.Length > 100)
                errors.Add(UserValidatorError.FullName);

            if (string.IsNullOrWhiteSpace(username) || username.Length > 100)
                errors.Add(UserValidatorError.Username);

            if (string.IsNullOrWhiteSpace(email) || !new EmailAddressAttribute().IsValid(email))
                errors.Add(UserValidatorError.Email);

            if (string.IsNullOrWhiteSpace(password) || !IsValidPassword(password))
                errors.Add(UserValidatorError.PaswordErr);

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
