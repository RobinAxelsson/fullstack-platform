using Microsoft.EntityFrameworkCore;
using TenStar.App.DataAccess;
using TenStar.App.Entities;
using TenStar.App.MessageDtos;
using TenStar.App.Messages;
using TenStar.App.MessagesResponse;

namespace TenStar.App
{
    internal static class UserManager
    {
        public static async Task<int> HandleRequestUserTableInsertMessage(RequestUserTableInsertMessage message, TenStarDbContext db)
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

        internal static async Task<RequestUsersResponse> HandleRequestUsersMessage(RequestUsersMessage requestUsersMessage, TenStarDbContext dbContext)
        {
            var users = await dbContext.Users.ToListAsync();

            if(users == null)
            {
                throw new ArgumentNullException(nameof(users));
            }

            var userDtos = users.Select(user => new UserDto {
                Email = user.Email,
                FullName = user.Name,
                Password = user.Password,
                Username = user.Username
            }).ToList();

            return new RequestUsersResponse(userDtos);
        }
    }
}
