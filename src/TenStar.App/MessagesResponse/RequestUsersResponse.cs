using TenStar.App.MessageDtos;

namespace TenStar.App.MessagesResponse
{
    public sealed class RequestUsersResponse : TenStarAppResponse
    {
        public IReadOnlyCollection<UserDto> UserDtos { get; }

        public RequestUsersResponse(IReadOnlyCollection<UserDto> users)
        {
            UserDtos = users;
        }
    }
}
