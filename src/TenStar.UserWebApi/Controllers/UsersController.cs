using Microsoft.AspNetCore.Mvc;
using static TenStar.UserContext.Api.Message.RequestCreateUsers;
using System.Text.Json;
using TenStar.UserContext.Api.Message;
using TenStar.UserContext;
using TenStar.UserContext.App.Exceptions;

namespace TenStar.UserWebApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserContextFacade _tenStarAppFacade;

        public UsersController(UserContextFacade tenStarAppFacade)
        {
            _tenStarAppFacade = tenStarAppFacade;
        }

        [HttpPost]
        public async Task<IActionResult> PostUsers([FromBody] List<DtoUser> users)
        {
            try
            {
                await _tenStarAppFacade.Handle(new RequestCreateUsers(users));

                return Ok(new { Message = "Users successfully added." });
            }
            catch (UserInvalidException ex)
            {
                return BadRequest(new
                {
                    Error = "Invalid User",
                    Details =  JsonSerializer.Serialize(ex.Errors)
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var response = await _tenStarAppFacade.Handle<RequestUsers, ResponseRequestCreateUsers>(new RequestUsers());
                if(response == null)
                {
                    throw new ArgumentNullException(nameof(response));
                }

                return Ok(response.UserDtos);
            }
            catch (UserInvalidException ex)
            {
                return BadRequest(new
                {
                    Error = "Invalid User",
                    Details = JsonSerializer.Serialize(ex.Errors)
                });
            }
        }
    }
}
