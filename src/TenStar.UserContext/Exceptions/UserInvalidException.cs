using TenStar.UserContext.Enum;

namespace TenStar.UserContext.Exceptions;

[Serializable]
public class UserInvalidException : TenStarUserContextException
{
    public IReadOnlyCollection<UserValidationError> Errors = Array.Empty<UserValidationError>();
    public UserInvalidException() { }
    public UserInvalidException(IEnumerable<UserValidationError> errors)
    {
        Errors = errors.ToArray();
    }
    public UserInvalidException(string message) : base(message) { }
    public UserInvalidException(string message, Exception inner) : base(message, inner) { }
}
