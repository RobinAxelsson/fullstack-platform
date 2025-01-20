namespace TenStar.App.Exceptions;

public abstract class TenStarAppException : Exception
{
    protected TenStarAppException() { }
    protected TenStarAppException(string message) : base(message) { }
    protected TenStarAppException(string message, Exception inner) : base(message, inner) { }
}
