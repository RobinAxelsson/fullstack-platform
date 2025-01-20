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
        var serviceProvider = new ServiceCollection()
            .AddDbContext<TenStarDbContext>();

        var services = serviceProvider.BuildServiceProvider();

        _dbContext = services.GetRequiredService<TenStarDbContext>();
    }

    public async void Handle<T>(T message) where T : TenStarMessage
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
