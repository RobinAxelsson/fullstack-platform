using TenStar.UserContext.Api.Exceptions;
using TenStar.UserContext.Api.Message;
using TenStar.UserContext.App;
using TenStar.UserContext.App.DataLayer;

namespace TenStar.UserContext;

public class UserContextFacade
{
    public async Task Handle<TMessage>(TMessage message) where TMessage : RequestUserContextBase
    {
        switch (message)
        {
            case RequestCreateUsers userTableInsertMessage:
                await UserManager.HandleRequestUserTableInsertMessage(userTableInsertMessage, new UserContextDbContext());
                break;

            default:
                throw new NotImplementedMessageException($"No handler implemented for message type: {typeof(TMessage).Name}");
        }
    }

    public async Task<TResponse> Handle<TMessage, TResponse>(TMessage message) where TMessage : RequestUserContextBase where TResponse : ResponseUserContextBase
    {
        switch (message)
        {
            case RequestUsers requestUsersMessage:
                return (TResponse)(object)await UserManager.HandleRequestUsersMessage(requestUsersMessage, new UserContextDbContext());

            default:
                throw new NotImplementedMessageException($"No handler implemented for message type: {typeof(TMessage).Name}");
        }
    }
}
