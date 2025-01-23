using Microsoft.EntityFrameworkCore;
using TenStar.UserContext.Api.Message;
using TenStar.UserContext.App.DataLayer;
using TenStar.UserContext.App.Entities;

namespace TenStar.UserContext.App
{
    internal static class UserManager
    {
        public static async Task<int> HandleRequestUserTableInsertMessage(RequestCreateUsers message, UserContextDbContext db)
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

        internal static async Task<ResponseRequestCreateUsers> HandleRequestUsersMessage(RequestUsers requestUsersMessage, UserContextDbContext dbContext)
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

            return new ResponseRequestCreateUsers(userDtos);
        }
    }
}
