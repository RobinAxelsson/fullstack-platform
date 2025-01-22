namespace TenStar.App.MessageDtos
{
    public record UserDto
    {
        //skip constructor when all is strings to avoid confusion
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Username { get; set; }
    }
}
