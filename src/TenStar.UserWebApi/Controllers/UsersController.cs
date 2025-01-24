﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.AspNetCore.Mvc;
using TenStar.App.Exceptions;
using TenStar.App;
using static TenStar.App.Messages.RequestUserTableInsertMessage;
using System.Text.Json;
using TenStar.App.MessageDtos;
using TenStar.App.Messages;
using TenStar.App.MessagesResponse;

namespace TenStar.UserWebApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly TenStarAppFacade _tenStarAppFacade;

        public UsersController(TenStarAppFacade tenStarAppFacade)
        {
            _tenStarAppFacade = tenStarAppFacade;
        }

        [HttpPost]
        public async Task<IActionResult> PostUsers([FromBody] List<UserDto> users)
        {
            try
            {
                await _tenStarAppFacade.Handle(new RequestUserTableInsertMessage(users));

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
                var response = await _tenStarAppFacade.Handle<RequestUsersMessage, RequestUsersResponse>(new RequestUsersMessage());
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
