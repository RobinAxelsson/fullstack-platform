using TenStar.App.Validators;

namespace TenStar.App.Exceptions;

[Serializable]
public class UserInvalidException : TenStarAppException
{
    public IReadOnlyCollection<UserValidatorError> Errors = Array.Empty<UserValidatorError>();
    public UserInvalidException() { }
    public UserInvalidException(IEnumerable<UserValidatorError> errors)
    {
        Errors = errors.ToArray();
    }
    public UserInvalidException(string message) : base(message) { }
    public UserInvalidException(string message, Exception inner) : base(message, inner){ }
}
