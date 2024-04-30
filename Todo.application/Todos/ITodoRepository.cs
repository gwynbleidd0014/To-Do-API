using Todo.application.Todos.Request;
using Todo.domain;
using Todo.domain.Todos;

namespace Todo.application.Todos;

public interface ITodoRepository
{
    Task<bool> RemoveAsync(CancellationToken token, int id);
    Task AddAsync(CancellationToken token, ToDo todo);
    Task<ToDo?> GetAsync(CancellationToken token, int id);
    Task<ToDo?> GetFullAsync(CancellationToken token, int id);
    Task<List<ToDo>> GetAllAsync(CancellationToken token, EntityStatus? status);
    Task UpdateAsync(CancellationToken token,  ToDo todo);
}
