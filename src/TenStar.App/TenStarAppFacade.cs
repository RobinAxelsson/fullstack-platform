using TenStar.App.DataAccess;
using TenStar.App.Exceptions;
using TenStar.App.Messages;
using TenStar.App.MessagesResponse;

namespace TenStar.App;

public class TenStarAppFacade
{
    private readonly TenStarDbContext _dbContext;

    public TenStarAppFacade()
    {
        _dbContext = new TenStarDbContext();
    }

    public async Task Handle<TMessage>(TMessage message) where TMessage : TenStarAppMessage
    {
        switch (message)
        {
            case RequestUserTableInsertMessage userTableInsertMessage:
                await UserManager.HandleRequestUserTableInsertMessage(userTableInsertMessage, _dbContext);
                break;

            default:
                throw new NotImplementedMessageException($"No handler implemented for message type: {typeof(TMessage).Name}");
        }
    }

    public async Task<TResponse> Handle<TMessage, TResponse>(TMessage message) where TMessage : TenStarAppMessage where TResponse : TenStarAppResponse
    {
        TResponse response = null;

        switch (message)
        {
            case RequestUsersMessage requestUsersMessage:
                return (TResponse)(object)(await UserManager.HandleRequestUsersMessage(requestUsersMessage, _dbContext));

            default:
                throw new NotImplementedMessageException($"No handler implemented for message type: {typeof(TMessage).Name}");
        }
    }
}
