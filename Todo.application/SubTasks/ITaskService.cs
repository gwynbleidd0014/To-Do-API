// Copyright (C) TBC Bank. All Rights Reserved.

using Todo.application.SubTasks.Request;
using Todo.application.SubTasks.Response;
using Todo.domain.SubTasks;

namespace Todo.application.SubTasks;

public interface ITaskService
{
    Task RemoveAsync(CancellationToken token, string id, string userId);
    Task AddAsync(CancellationToken token, SubTaskRequestModel task, string userId);
    Task<SubTaskResponseModel> GetAsync(CancellationToken token, string id, string userId);
    Task UpdateAsync(CancellationToken token, SubTaskPutModel task, string id, string userId);
}
