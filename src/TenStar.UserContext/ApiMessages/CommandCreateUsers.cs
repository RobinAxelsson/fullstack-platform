namespace TenStar.UserContext.ApiMessages
{
    public sealed partial class CommandCreateUsers : ApiMessageBase
    {
        internal DtoUser[] UserDtos { get; } = [];

        public CommandCreateUsers(IEnumerable<DtoUser> users)
        {
            UserDtos = users.ToArray();
        }
    }
}
