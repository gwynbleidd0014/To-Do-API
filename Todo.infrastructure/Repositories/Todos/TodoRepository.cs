using Microsoft.EntityFrameworkCore;
using Todo.application.SubTasks;
using Todo.application.Todos;
using Todo.domain;
using Todo.domain.Todos;
using Todo.persistance.Context;

namespace Todo.infrastructure.Repositories.Todos;

public class TodoRepository : BaseRepository<ToDo>, ITodoRepository
{
    private readonly ITaskRepository _taskRepository;
    public TodoRepository(TodoContext context, ITaskRepository taskRepository) : base(context)
    {
        _taskRepository = taskRepository;
    }

    public async Task<ToDo?> GetFullAsync(CancellationToken token, int id)
    {
        var todo = await _dbset
            .Include(x => x.SubTasks)
            .SingleOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);

        return todo;
    }

    public async Task<ToDo?> GetAsync(CancellationToken token, int id)
    {
        var todo = await _dbset
            .SingleOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);

        return todo;
    }

    public async Task<bool> RemoveAsync(CancellationToken token, int id)
    {
        var todo = await _dbset
            .Include(x => x.SubTasks)
            .SingleOrDefaultAsync(x =>  x.Id == id).ConfigureAwait(false);

        await base.RemoveAsync(token, id).ConfigureAwait(false);

        if (todo is null)
            return false;

        foreach (var task in todo.SubTasks)
        {
            task.Status = EntityStatus.Deleted;
            await _taskRepository.UpdateAsync(token, task).ConfigureAwait(false);
        }

        return true;
    }

    public Task UpdateAsync(CancellationToken token, ToDo todo)
    {
        base.Update(todo);
        return Task.CompletedTask;
    }

    public new async Task AddAsync(CancellationToken token, ToDo todo)
    {
        todo.Status = EntityStatus.Active;
        await base.AddAsync(token, todo).ConfigureAwait(false);
    }

    public async Task<List<ToDo>> GetAllAsync(CancellationToken token, EntityStatus? status)
    {
        if (status is null)
            return await _dbset.Include(x => x.SubTasks).ToListAsync().ConfigureAwait(false);

        return await _dbset
             .Include(x => x.SubTasks)
             .Where(x => x.Status == status)
             .ToListAsync().ConfigureAwait(false);
    }
}
