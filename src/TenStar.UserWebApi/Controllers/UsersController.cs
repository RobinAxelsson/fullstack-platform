using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TenStar.UserContext;
using TenStar.UserContext.Exceptions;
using TenStar.UserContext.ApiMessages;

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
                await _tenStarAppFacade.Handle(new CommandCreateUsers(users));
                //TODO: Add a response message
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
                var query = new QueryUsers();
                await _tenStarAppFacade.Handle(query);
                return Ok(query.GetResult());
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
