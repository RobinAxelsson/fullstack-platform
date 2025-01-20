using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TenStar.App.DataAccess;
using TenStar.App.Exceptions;
using TenStar.App.Messages;

namespace TenStar.App;

public sealed class TenStar
{
    private readonly TenStarDbContext _dbContext;

    internal TenStar()
    {
        _dbContext = new TenStarDbContext();
    }

    public async void Handle<T>(T message) where T : TenStarAppMessage
    {
        switch (message)
        {
            case RequestUserTableInsertMessage userTableInsertMessage:
                await UserManager.HandleRequestUserTableInsertMessage(userTableInsertMessage, _dbContext.AddRange, () => _dbContext.SaveChangesAsync());
                break;

            default:
                throw new NotImplementedMessageException();
        }
    }
}
