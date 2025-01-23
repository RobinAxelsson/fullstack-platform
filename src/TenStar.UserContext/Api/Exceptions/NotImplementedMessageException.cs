using TenStar.UserContext.App.Exceptions;

namespace TenStar.UserContext.Api.Exceptions;

public class NotImplementedMessageException : TenStarUserProjectException
{
    public NotImplementedMessageException() { }
    public NotImplementedMessageException(string message) : base(message) { }
    public NotImplementedMessageException(string message, Exception inner) : base(message, inner) { }
}
