namespace TenStar.UserContext.Exceptions;

public class NotImplementedApiMessageException : TenStarUserContextException
{
    public NotImplementedApiMessageException() { }
    public NotImplementedApiMessageException(string message) : base(message) { }
    public NotImplementedApiMessageException(string message, Exception inner) : base(message, inner) { }
}
