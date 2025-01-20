using TenStar.App.Entities;
using TenStar.App.Messages;

namespace TenStar.App
{
    internal static class UserManager
    {
        public static async Task<int> HandleRequestUserTableInsertMessage(RequestUserTableInsertMessage message, Action<List<User>> addUsers, Func<Task<int>> saveChangesAsync)
        {
            var users = message.UserDtos.Select(userDto => new User(
                userDto.Name,
                userDto.Email,
                userDto.Password,
                userDto.Username
            )).ToList();
            addUsers(users);
            return await saveChangesAsync();
        }
    }
}
