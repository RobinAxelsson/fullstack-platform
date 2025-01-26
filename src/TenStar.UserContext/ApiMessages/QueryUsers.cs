namespace TenStar.UserContext.ApiMessages
{
    public sealed class QueryUsers : ApiMessageBase
    {
        internal DtoUser[] Users { get; set; } = [];
        public DtoUser[] GetResult() => Users;
    }
}
