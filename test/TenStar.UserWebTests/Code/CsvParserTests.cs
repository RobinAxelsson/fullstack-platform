using System.Text;
using TenStar.UserWeb.Code;

namespace TenStar.UserWebTests.Code
{
    public class CsvParserTests
    {
        [Fact]
        public async Task ParseUsers_ValidCsv_ReturnsUsers()
        {
            // Arrange
            var csvData = "FullName,Username,Email,Password\n" +
                          "John Doe,johndoe,john.doe@example.com,Password123\n" +
                          "Jane Smith,janesmith,jane.smith@example.com,SecurePass456";

            await using var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(csvData));

            // Act
            var users = await CsvParser.ParseUsers(memoryStream);

            // Assert
            Assert.NotNull(users);
            Assert.Equal(2, users.Length);
            Assert.Equal("John Doe", users[0].FullName);
            Assert.Equal("johndoe", users[0].Username);
            Assert.Equal("john.doe@example.com", users[0].Email);
            Assert.Equal("Password123", users[0].Password);
            Assert.Equal("Jane Smith", users[1].FullName);
            Assert.Equal("janesmith", users[1].Username);
            Assert.Equal("jane.smith@example.com", users[1].Email);
            Assert.Equal("SecurePass456", users[1].Password);
        }

        [Fact]
        public async Task ParseUsers_EmptyStream_ThrowsException()
        {
            // Arrange
            await using var memoryStream = new MemoryStream();

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => CsvParser.ParseUsers(memoryStream));
        }

        [Fact]
        public async Task ParseUsers_InvalidCsv_ReturnsEmptyArray()
        {
            // Arrange
            var csvData = "Invalid,Data,Only"; // Less than 4 columns

            await using var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(csvData));

            // Act
            var users = await CsvParser.ParseUsers(memoryStream);

            // Assert
            Assert.NotNull(users);
            Assert.Empty(users);
        }

        [Fact]
        public async Task ParseUsers_NullStream_ThrowsArgumentNullException()
        {
            // Arrange
            Stream? nullStream = null;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => CsvParser.ParseUsers(nullStream));
        }

        [Fact]
        public async Task ParseUsers_UnreadableStream_ThrowsInvalidOperationException()
        {
            // Arrange
            var unreadableStream = new MemoryStream();
            unreadableStream.Close();

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => CsvParser.ParseUsers(unreadableStream));
        }

        [Fact]
        public async Task ParseUsers_EmptyCsvContent_ReturnsEmptyArray()
        {
            // Arrange
            var csvData = "";

            await using var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(csvData));

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => CsvParser.ParseUsers(memoryStream));
        }
    }

    public class User
    {
        public string FullName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
