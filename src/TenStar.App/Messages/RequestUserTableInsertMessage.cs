using TenStar.App.MessageDtos;

namespace TenStar.App.Messages
{
    public sealed partial class RequestUserTableInsertMessage : TenStarAppMessage
    {
        internal IReadOnlyCollection<UserDto> UserDtos { get; }

        public RequestUserTableInsertMessage(IReadOnlyCollection<UserDto> users)
        {
            UserDtos = users;
        }
    }
}
