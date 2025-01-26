using TenStar.UserContext.ApiMessages;
using TenStar.UserContext.DataLayer;
using TenStar.UserContext.Exceptions;
using TenStar.UserContext.Managers;

namespace TenStar.UserContext;

public class UserContextFacade
{
    public async Task Handle<TMessage>(TMessage message) where TMessage : ApiMessageBase
    {
        switch (message)
        {
            case QueryUsers requestUsersMessage:
                await UserManager.HandleRequestUsersMessage(requestUsersMessage, new UserContextDbContext());
                break;

            default:
                throw new NotImplementedApiMessageException($"No handler implemented for message type: {typeof(TMessage).Name}");
        }

        throw new NotImplementedException();
    }
}
