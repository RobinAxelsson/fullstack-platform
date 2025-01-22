using TenStar.App.DataAccess;
using TenStar.App.Exceptions;
using TenStar.App.Messages;

namespace TenStar.App;

public class TenStarAppFacade
{
    private readonly TenStarDbContext _dbContext;

    public TenStarAppFacade()
    {
        _dbContext = new TenStarDbContext();
    }

    public async Task Handle<T>(T message) where T : TenStarAppMessage
    {
        switch (message)
        {
            case RequestUserTableInsertMessage userTableInsertMessage:
                await UserManager.HandleRequestUserTableInsertMessage(userTableInsertMessage, _dbContext);
                break;

            default:
                throw new NotImplementedMessageException();
        }
    }
}
