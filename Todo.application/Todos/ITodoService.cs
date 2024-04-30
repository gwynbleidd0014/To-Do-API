using Microsoft.AspNetCore.JsonPatch;
using Todo.application.Todos.Request;
using Todo.application.Todos.Response;
using Todo.domain.Todos;

namespace Todo.application.Todos;

public interface ITodoService
{
    Task RemoveAsync(CancellationToken token, string id, string userId);
    Task AddAsync(CancellationToken token, TodoPutModel todo, string ownerId);
    Task<TodoResponseModel> GetAsync(CancellationToken token, string id, string userId);
    Task<List<TodoResponseModel>> GetAllAsync(CancellationToken token, string? status);
    Task UpdateAsync<T>(CancellationToken token, T todo, string todoId, string userId);
    Task UpdateStatusAsync(CancellationToken token, string id, string userId);
    Task<bool> ValidateUser(CancellationToken token, string todoId, string userId);
}
