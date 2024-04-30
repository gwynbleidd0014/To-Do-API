// Copyright (C) TBC Bank. All Rights Reserved.

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Todo.api.infrastructure.Auth;
using Todo.application.Todos;
using Todo.application.Todos.Request;
using Todo.application.Todos.Response;

namespace Todo.api.Controllers.v1;

[Route("v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize]
public class TodoController : ControllerBase
{
    private readonly ITodoService _todoService;

    public TodoController(ITodoService todoService)
    {
        _todoService = todoService;
    }

    [HttpPost]
    public async Task AddAsync(CancellationToken token, [FromBody] TodoPutModel todo)
    {
        await _todoService.AddAsync(token, todo, JwtHelper.ParseClaims(Request)["Id"]).ConfigureAwait(false);
    }

    [HttpGet]
    public async Task<List<TodoResponseModel>> GetAllAsync(CancellationToken token, [FromQuery] string? status)
    {
        var todos = await _todoService.GetAllAsync(token, status).ConfigureAwait(false);
        return todos;
    }

    [HttpPut("{id}")]
    public async Task UpdateAsync(CancellationToken token, [FromBody] TodoRequestModel todo, string id)
    {
        await _todoService.UpdateAsync(token, todo, id, JwtHelper.ParseClaims(Request)["Id"]).ConfigureAwait(false);
    }

    [HttpPatch("{id}")]
    public async Task UpdatePartialAsync(CancellationToken token, [FromBody] TodoPatchModel todo, string id)
    {
        await _todoService.UpdateAsync(token, todo, id, JwtHelper.ParseClaims(Request)["Id"]).ConfigureAwait(false);
    }

    [HttpGet("{id}")]
    public async Task<TodoResponseModel> GetAsync(CancellationToken token, string id)
    {
        return await _todoService.GetAsync(token, id, JwtHelper.ParseClaims(Request)["Id"]).ConfigureAwait(false);
    }

    [HttpGet("{id}/done")]
    public async Task Update(CancellationToken token, string id)
    {
        await _todoService.UpdateStatusAsync(token, id, JwtHelper.ParseClaims(Request)["Id"]).ConfigureAwait(false);
    }

    [HttpDelete("{id}")]
    public async Task Delete(CancellationToken token, string id)
    {
        await _todoService.RemoveAsync(token, id, JwtHelper.ParseClaims(Request)["Id"]).ConfigureAwait(false);
    }
}
