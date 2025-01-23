namespace TenStar.UserContext.App.Exceptions;

public abstract class TenStarUserProjectException : Exception
{
    protected TenStarUserProjectException() { }
    protected TenStarUserProjectException(string message) : base(message) { }
    protected TenStarUserProjectException(string message, Exception inner) : base(message, inner) { }
}
