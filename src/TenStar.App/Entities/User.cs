using TenStar.App.Exceptions;
using TenStar.App.Validators;

namespace TenStar.App.Entities
{
    public sealed class User
    {
        [Obsolete("This constructor is EF only to maintain consistent object creation", error: true)]
        public User() { }
        public int DbId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }

        internal User(string name, string email, string password, string username)
        {

            var errors = UserValidator.ValidateUserInput(name, email, password, username);

            if(errors.Count  > 0)
                throw new UserInvalidException(errors);

            Name = name;
            Email = email;
            Password = password;
            Username = username;
        }
    }
}
