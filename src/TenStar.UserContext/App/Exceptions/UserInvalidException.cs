namespace TenStar.UserContext.App.Exceptions;

[Serializable]
public class UserInvalidException : TenStarUserProjectException
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

public enum UserValidationError
{
    FullName,
    Username,
    Email,
    Pasword
}
