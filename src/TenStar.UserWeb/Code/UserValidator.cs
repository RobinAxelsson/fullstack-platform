using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace TenStar.UserWeb.Code
{
    public static class UserValidator
    {
        public static bool ValidateUser(User? user)
        {
            return user != null &&
                   ValidateFullName(user.FullName) &&
                   ValidateUsername(user.Username) &&
                   ValidateEmail(user.Email) &&
                   ValidatePassword(user.Password);
        }

        public static bool ValidateFullName(string? fullName) => !(string.IsNullOrWhiteSpace(fullName) || fullName.Length > 100);

        public static bool ValidateUsername(string? username) => !(string.IsNullOrWhiteSpace(username) || username.Length > 100);

        public static bool ValidateEmail(string? email) => !string.IsNullOrWhiteSpace(email) && new EmailAddressAttribute().IsValid(email);

        public static bool ValidatePassword(string? password) => !string.IsNullOrWhiteSpace(password) && IsValidPassword(password);

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
