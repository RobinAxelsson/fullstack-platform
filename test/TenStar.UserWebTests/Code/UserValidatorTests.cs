using TenStar.UserWeb.Code;

namespace TenStar.UserWebTests.Code
{
    public class UserValidatorTests
    {
        [Fact]
        public void ValidateFullName_ValidFullName_ReturnsTrue()
        {
            // Arrange
            string fullName = "John Doe";

            // Act
            bool result = UserValidator.ValidateFullName(fullName);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ValidateFullName_EmptyOrTooLong_ReturnsFalse()
        {
            // Arrange
            string emptyFullName = "";
            string longFullName = new string('A', 101);

            // Act
            bool resultEmpty = UserValidator.ValidateFullName(emptyFullName);
            bool resultLong = UserValidator.ValidateFullName(longFullName);

            // Assert
            Assert.False(resultEmpty);
            Assert.False(resultLong);
        }

        [Fact]
        public void ValidateUsername_ValidUsername_ReturnsTrue()
        {
            // Arrange
            string username = "User123";

            // Act
            bool result = UserValidator.ValidateUsername(username);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ValidateUsername_EmptyOrTooLong_ReturnsFalse()
        {
            // Arrange
            string emptyUsername = "";
            string longUsername = new string('B', 101);

            // Act
            bool resultEmpty = UserValidator.ValidateUsername(emptyUsername);
            bool resultLong = UserValidator.ValidateUsername(longUsername);

            // Assert
            Assert.False(resultEmpty);
            Assert.False(resultLong);
        }

        [Fact]
        public void ValidateEmail_ValidEmail_ReturnsTrue()
        {
            // Arrange
            string email = "example@test.com";

            // Act
            bool result = UserValidator.ValidateEmail(email);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ValidateEmail_InvalidEmail_ReturnsFalse()
        {
            // Arrange
            string invalidEmail = "not-an-email";

            // Act
            bool result = UserValidator.ValidateEmail(invalidEmail);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ValidatePassword_ValidPassword_ReturnsTrue()
        {
            // Arrange
            string password = "StrongP@ssw0rd";

            // Act
            bool result = UserValidator.ValidatePassword(password);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ValidatePassword_InvalidPassword_ReturnsFalse()
        {
            // Arrange
            string noUpperCase = "weakpassword1!";
            string noLowerCase = "WEAKPASSWORD1!";
            string noDigit = "WeakPassword!";
            string noSpecialChar = "WeakPassword1";
            string tooShort = "P@1a";

            // Act
            bool resultNoUpperCase = UserValidator.ValidatePassword(noUpperCase);
            bool resultNoLowerCase = UserValidator.ValidatePassword(noLowerCase);
            bool resultNoDigit = UserValidator.ValidatePassword(noDigit);
            bool resultNoSpecialChar = UserValidator.ValidatePassword(noSpecialChar);
            bool resultTooShort = UserValidator.ValidatePassword(tooShort);

            // Assert
            Assert.False(resultNoUpperCase);
            Assert.False(resultNoLowerCase);
            Assert.False(resultNoDigit);
            Assert.False(resultNoSpecialChar);
            Assert.False(resultTooShort);
        }

        [Fact]
        public void ValidateUser_ValidUser_ReturnsTrue()
        {
            // Arrange
            var user = new User
            {
                FullName = "John Doe",
                Username = "johndoe123",
                Email = "johndoe@example.com",
                Password = "Str0ngP@ss!"
            };

            // Act
            bool result = UserValidator.ValidateUser(user);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ValidateUser_InvalidUser_ReturnsFalse()
        {
            // Arrange
            var user = new User
            {
                FullName = "", // Invalid
                Username = "johndoe123",
                Email = "johndoe@example.com",
                Password = "Str0ngP@ss!"
            };

            // Act
            bool result = UserValidator.ValidateUser(user);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ValidateUser_NullUser_ReturnsFalse()
        {
            // Arrange
            User user = null;

            // Act
            bool result = UserValidator.ValidateUser(user);

            // Assert
            Assert.False(result);
        }
    }
}
