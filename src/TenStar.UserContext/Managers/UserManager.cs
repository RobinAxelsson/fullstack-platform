using Microsoft.EntityFrameworkCore;
using TenStar.UserContext.ApiMessages;
using TenStar.UserContext.DataLayer;
using TenStar.UserContext.Entities;

namespace TenStar.UserContext.Managers
{
    internal static class UserManager
    {
        public static async Task<int> HandleRequestUserTableInsertMessage(CommandCreateUsers message, UserContextDbContext db)
        {
            var users = message.UserDtos.Select(userDto => new User(
                userDto.FullName,
                userDto.Email,
                userDto.Password,
                userDto.Username
            )).ToList();
            db.Users.AddRange(users);
            return await db.SaveChangesAsync();
        }

        internal static async Task HandleRequestUsersMessage(QueryUsers requestUsersMessage, UserContextDbContext dbContext)
        {
            var users = await dbContext.Users.AsNoTracking().ToListAsync();

            if (users == null)
            {
                throw new ArgumentNullException(nameof(users));
            }

            var userDtos = users.Select(user => new DtoUser
            {
                Email = user.Email,
                FullName = user.Name,
                Password = user.Password,
                Username = user.Username
            }).ToList();
        }
    }
}
