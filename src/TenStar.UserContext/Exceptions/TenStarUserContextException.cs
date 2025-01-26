namespace TenStar.UserContext.Exceptions;

public abstract class TenStarUserContextException : Exception
{
    protected TenStarUserContextException() { }
    protected TenStarUserContextException(string message) : base(message) { }
    protected TenStarUserContextException(string message, Exception inner) : base(message, inner) { }
}
