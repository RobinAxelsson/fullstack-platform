using System.Runtime.Serialization;

namespace TenStar.App.Exceptions;

[Serializable]
public class UserInvalidException : TenStarAppException
{
    public UserInvalidException() { }
    public UserInvalidException(string message) : base(message) { }
    public UserInvalidException(string message, Exception inner) : base(message, inner) { }
}
