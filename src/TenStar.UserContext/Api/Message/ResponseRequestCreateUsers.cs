namespace TenStar.UserContext.Api.Message
{
    public sealed class ResponseRequestCreateUsers : ResponseUserContextBase
    {
        public IReadOnlyCollection<DtoUser> UserDtos { get; }

        public ResponseRequestCreateUsers(IReadOnlyCollection<DtoUser> users)
        {
            UserDtos = users;
        }
    }
}
