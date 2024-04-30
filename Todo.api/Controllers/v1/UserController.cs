// Copyright (C) TBC Bank. All Rights Reserved.
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Todo.api.infrastructure.Auth;
using Todo.application.Users;
using Todo.application.Users.Request;
using Todo.application.Users.Response;

namespace Todo.api.Controllers.v1
{
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IOptions<AuthConfig> _options;

        public UserController(IUserService userService, IOptions<AuthConfig> options)
        {
            _userService = userService;
            _options = options;
        }

        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<string> Login([FromBody] UserRequestModel user, CancellationToken token)
        {
            var result = await _userService.LogIn(token, user).ConfigureAwait(false);
            return JwtHelper.GenerateToken(result.UserName, result.Id, _options);
        }

        [Route("register")]
        [AllowAnonymous]
        [HttpPost]
        public async Task Register([FromBody] UserRequestModel user, CancellationToken token)
        {
            await _userService.Register(token, user).ConfigureAwait(false);
        }


        [HttpPut]
        public async Task Update([FromBody] UserUpdateModel model, CancellationToken token)
        {
            await _userService.Update(token, model, JwtHelper.ParseClaims(Request)["Id"]).ConfigureAwait(false);
        }

        [HttpDelete]
        public async Task Delete(CancellationToken token)
        {
            await _userService.RemoveAsync(token, JwtHelper.ParseClaims(Request)["Id"]).ConfigureAwait(false);
        }

        [HttpGet]
        public async Task<UserResponseModel> GetAsync(CancellationToken token)
        {
           return await _userService.GetAsync(token, JwtHelper.ParseClaims(Request)["Id"]).ConfigureAwait(false);
        }
    }
}
