using TenStar.App.Validators;

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
        [InlineData(null, "email@example.com", "StrongPass1!", "username", UserValidatorError.FullName)]
        [InlineData("", "email@example.com", "StrongPass1!", "username", UserValidatorError.FullName)]
        [InlineData(" ", "email@example.com", "StrongPass1!", "username", UserValidatorError.FullName)]
        [InlineData("A very long name that exceeds the character limit of 100 characters for validation purposesssssssssssssssssssssssssssssssssssss.", "email@example.com", "StrongPass1!", "username", UserValidatorError.FullName)]
        [InlineData("Valid Name", "", "StrongPass1!", "username", UserValidatorError.Email)]
        [InlineData("Valid Name", "invalid-email", "StrongPass1!", "username", UserValidatorError.Email)]
        [InlineData("Valid Name", "email@example.com", "weakpass", "username", UserValidatorError.PaswordErr)]
        [InlineData("Valid Name", "email@example.com", "StrongPass1!", "", UserValidatorError.Username)]
        [InlineData("Valid Name", "email@example.com", "StrongPass1!", "A very long username that exceeds the character limit of 100 characters for validation purposesssssssssssssssssssss.", UserValidatorError.Username)]
        public void ValidateUserInput_InvalidInput_ShouldReturnExpectedErrors(string name, string email, string password, string username, UserValidatorError expectedError)
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
            Assert.Contains(UserValidatorError.FullName, errors);
            Assert.Contains(UserValidatorError.Email, errors);
            Assert.Contains(UserValidatorError.PaswordErr, errors);
            Assert.Contains(UserValidatorError.Username, errors);
        }
    }
}
