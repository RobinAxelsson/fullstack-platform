
using TenStar.UserContext.App.Exceptions;
using TenStar.UserContext.App.Validators;

namespace TenStar.App.Tests
{
    public class UserValidatorTests
    {
        [Fact]
        public void ValidateUserInput_ValidInput_ShouldReturnNoErrors()
        {
            // Arrange
            string name = "John Doe";
            string email = "john.doe@example.com";
            string password = "StrongPass1!";
            string username = "johndoe";

            // Act
            var errors = UserValidator.ValidateUserInput(name, email, password, username);

            // Assert
            Assert.NotNull(errors);
            Assert.Empty(errors);
        }

        [Theory]
        [InlineData("", "email@example.com", "StrongPass1!", "username", UserValidationError.FullName)]
        [InlineData(" ", "email@example.com", "StrongPass1!", "username", UserValidationError.FullName)]
        [InlineData("A very long name that exceeds the character limit of 100 characters for validation purposesssssssssssssssssssssssssssssssssssss.", "email@example.com", "StrongPass1!", "username", UserValidationError.FullName)]
        [InlineData("Valid Name", "", "StrongPass1!", "username", UserValidationError.Email)]
        [InlineData("Valid Name", "invalid-email", "StrongPass1!", "username", UserValidationError.Email)]
        [InlineData("Valid Name", "email@example.com", "weakpass", "username", UserValidationError.Pasword)]
        [InlineData("Valid Name", "email@example.com", "StrongPass1!", "", UserValidationError.Username)]
        [InlineData("Valid Name", "email@example.com", "StrongPass1!", "A very long username that exceeds the character limit of 100 characters for validation purposesssssssssssssssssssss.", UserValidationError.Username)]
        public void ValidateUserInput_InvalidInput_ShouldReturnExpectedErrors(string name, string email, string password, string username, UserValidationError expectedError)
        {
            // Act
            var errors = UserValidator.ValidateUserInput(name, email, password, username);

            // Assert
            Assert.NotNull(errors);
            Assert.Contains(expectedError, errors);
        }

        [Fact]
        public void ValidateUserInput_MultipleInvalidInputs_ShouldReturnAllExpectedErrors()
        {
            // Arrange
            string name = ""; // Invalid
            string email = "invalid-email"; // Invalid
            string password = "weakpass"; // Invalid
            string username = ""; // Invalid

            // Act
            var errors = UserValidator.ValidateUserInput(name, email, password, username);

            // Assert
            Assert.NotNull(errors);
            Assert.Contains(UserValidationError.FullName, errors);
            Assert.Contains(UserValidationError.Email, errors);
            Assert.Contains(UserValidationError.Pasword, errors);
            Assert.Contains(UserValidationError.Username, errors);
        }
    }
}
