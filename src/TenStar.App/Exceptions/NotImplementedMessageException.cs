namespace TenStar.App.Exceptions;

public class NotImplementedMessageException : TenStarAppException
{
    public NotImplementedMessageException() { }
    public NotImplementedMessageException(string message) : base(message) { }
    public NotImplementedMessageException(string message, Exception inner) : base(message, inner) { }
}
