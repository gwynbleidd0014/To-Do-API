// Copyright (C) TBC Bank. All Rights Reserved.

using Todo.domain.SubTasks;

namespace Todo.application.SubTasks;

public interface ITaskRepository
{
    Task RemoveAsync(CancellationToken token, int id);
    Task AddAsync(CancellationToken token, SubTask task);
    Task<SubTask?> GetAsync(CancellationToken token, int id);
    Task UpdateAsync(CancellationToken token, SubTask task);
}
