using TenStar.UserWeb.Code;


namespace TenStar.UserWeb.Tests
{
    public class CsvParserTests
    {
        [Fact]
        public async Task ParseUsers_HappyPath_ShouldReturnParsedUsers()
        {
            // Arrange
            var csvFilePath = "./data/happy-path.csv";
            await using var fileStream = new FileStream(csvFilePath, FileMode.Open, FileAccess.Read);

            // Act
            var users = await CsvParser.ParseUsers(fileStream);

            // Assert
            Assert.NotNull(users);
            Assert.Equal(2, users.Length);
            Assert.Equal("John Doe", users[0].FullName);
            Assert.Equal("johndoe", users[0].Username);
            Assert.Equal("john.doe@example.com", users[0].Email);
            Assert.Equal("password123", users[0].Password);
        }

        [Fact]
        public async Task ParseUsers_BrokenFile_ShouldReturnEmptyArray()
        {
            // Arrange
            var csvFilePath = "./data/broken.csv";
            await using var fileStream = new FileStream(csvFilePath, FileMode.Open, FileAccess.Read);

            // Act
            var users = await CsvParser.ParseUsers(fileStream);

            // Assert
            Assert.NotNull(users);
            Assert.Empty(users);
        }

        [Fact]
        public async Task ParseUsers_EmptyStream_ShouldReturnEmptyArray()
        {
            // Arrange
            using var emptyStream = new MemoryStream();

            // Act
            var users = await CsvParser.ParseUsers(emptyStream);

            // Assert
            Assert.NotNull(users);
            Assert.Empty(users);
        }

        [Fact]
        public async Task ParseUsers_NullStream_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await CsvParser.ParseUsers(null));
        }
    }
}
