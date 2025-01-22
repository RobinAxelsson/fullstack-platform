namespace TenStar.App.Messages
{
    public sealed class RequestUserTableInsertMessage : TenStarAppMessage
    {
        internal IReadOnlyCollection<UserDto> UserDtos { get; }

        public RequestUserTableInsertMessage(IReadOnlyCollection<UserDto> users)
        {
            UserDtos = users;
        }

        public record UserDto
        {
            public required string Name { get; set; }
            public required string Email { get; set; }
            public required string Password { get; set; }
            public required string Username { get; set; }
        }
    }
}
