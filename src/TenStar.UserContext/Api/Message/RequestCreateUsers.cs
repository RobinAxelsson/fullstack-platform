namespace TenStar.UserContext.Api.Message
{
    public sealed partial class RequestCreateUsers : RequestUserContextBase
    {
        internal IReadOnlyCollection<DtoUser> UserDtos { get; }

        public RequestCreateUsers(IReadOnlyCollection<DtoUser> users)
        {
            UserDtos = users;
        }
    }
}
