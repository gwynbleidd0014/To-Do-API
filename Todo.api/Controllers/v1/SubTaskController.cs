// Copyright (C) TBC Bank. All Rights Reserved.

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.api.infrastructure.Auth;
using Todo.application.SubTasks;
using Todo.application.SubTasks.Request;
using Todo.application.SubTasks.Response;

namespace Todo.api.Controllers.v1
{
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class SubTaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public SubTaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        public async Task Add(CancellationToken token, [FromBody] SubTaskRequestModel task)
        {
            await _taskService.AddAsync(token, task, JwtHelper.ParseClaims(Request)["Id"]).ConfigureAwait(false);
        }

        [HttpGet("{id}")]
        public async Task<SubTaskResponseModel> Get(CancellationToken token, string id)
        {
           return await _taskService.GetAsync(token, id, JwtHelper.ParseClaims(Request)["Id"]).ConfigureAwait(false);
        }

        [HttpDelete("{id}")]
        public async Task Remove(CancellationToken token, string id)
        {
            await _taskService.RemoveAsync(token, id, JwtHelper.ParseClaims(Request)["Id"]).ConfigureAwait(false);
        }

        [HttpPut("{id}")]
        public async Task Update(CancellationToken token, SubTaskPutModel task, string id)
        {
            await _taskService.UpdateAsync(token, task, id, JwtHelper.ParseClaims(Request)["Id"]).ConfigureAwait(false);
        }
    }
}
