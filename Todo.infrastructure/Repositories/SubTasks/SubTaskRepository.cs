// Copyright (C) TBC Bank. All Rights Reserved.

using Todo.application.SubTasks;
using Todo.domain.SubTasks;
using Todo.persistance.Context;
using Todo.domain;

namespace Todo.infrastructure.Repositories.SubTasks;

public class SubTaskRepository : BaseRepository<SubTask>, ITaskRepository
{
    public SubTaskRepository(TodoContext context) : base(context)
    {
        
    }
    public async Task<SubTask?> GetAsync(CancellationToken token, int id)
    {
        return await base.GetAsync(token, id).ConfigureAwait(false);
    }

    public async Task RemoveAsync(CancellationToken token, int id)
    {
        await base.RemoveAsync(token, id).ConfigureAwait(false);
    }
    public async Task UpdateAsync(CancellationToken token, SubTask task)
    {
        await base.Update(task).ConfigureAwait(false);
    }
    public async new Task AddAsync(CancellationToken token, SubTask task)
    {
        task.Status = EntityStatus.Active;
        await base.AddAsync(token, task).ConfigureAwait(false);
    }
}
