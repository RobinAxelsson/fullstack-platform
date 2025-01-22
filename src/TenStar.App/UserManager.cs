using TenStar.App.DataAccess;
using TenStar.App.Entities;
using TenStar.App.Messages;

namespace TenStar.App
{
    internal static class UserManager
    {
        public static async Task<int> HandleRequestUserTableInsertMessage(RequestUserTableInsertMessage message, TenStarDbContext db)
        {
            var users = message.UserDtos.Select(userDto => new User(
                userDto.Name,
                userDto.Email,
                userDto.Password,
                userDto.Username
            )).ToList();
            db.Users.AddRange(users);
            return await db.SaveChangesAsync();
        }
    }
}
