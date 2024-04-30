// Copyright (C) TBC Bank. All Rights Reserved.

using Mapster;
using Todo.application.Abstractions;
using Todo.application.Exceptions.CustomExceptions;
using Todo.application.Exceptions.ErrorMessages;
using Todo.application.Helpers;
using Todo.application.SubTasks.Request;
using Todo.application.SubTasks.Response;
using Todo.application.Todos;
using Todo.domain.SubTasks;

namespace Todo.application.SubTasks;

public class SubTaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly ITodoService _todoService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHid _hid;
    public SubTaskService(ITaskRepository taskRepository, ITodoService todoService, IUnitOfWork unitOfWork, IHid hid)
    {
        _taskRepository = taskRepository;
        _todoService = todoService;
        _unitOfWork = unitOfWork;
        _hid = hid;
    }

    public async Task AddAsync(CancellationToken token, SubTaskRequestModel task, string userId)
    {
        var isUsersTodo = await _todoService.ValidateUser(token, task.TodoId, userId).ConfigureAwait(false);
        if (!isUsersTodo)
            throw new NotFound(ErrorMessages.TaskNotFound);

        await _taskRepository.AddAsync(token, task.Adapt<SubTask>()).ConfigureAwait(false);

        _unitOfWork.SaveChanges();
    }
    public async Task<SubTaskResponseModel> GetAsync(CancellationToken token, string id, string userId)
    {
        var task = await _taskRepository.GetAsync(token, _hid.Decode(id)).ConfigureAwait(false) ??
            throw new NotFound(ErrorMessages.TaskNotFound);

        var isUsersTodo = await _todoService.ValidateUser(token, _hid.Encode(task.ToDoId), userId).ConfigureAwait(false);
        if (!isUsersTodo)
            throw new NotFound(ErrorMessages.TaskNotFound);

        return task.Adapt<SubTaskResponseModel>();
    }


    public async Task RemoveAsync(CancellationToken token, string id, string userId)
    {
        var task = await _taskRepository.GetAsync(token, _hid.Decode(id)).ConfigureAwait(false) ??
            throw new NotFound(ErrorMessages.TaskNotFound); ;

        var isUsersTodo = await _todoService.ValidateUser(token, _hid.Encode(task.ToDoId), userId).ConfigureAwait(false);
        if (!isUsersTodo)
            throw new NotFound(ErrorMessages.TaskNotFound);

        await _taskRepository.RemoveAsync(token, _hid.Decode(id)).ConfigureAwait(false);

        _unitOfWork.SaveChanges();
    }
    public async Task UpdateAsync(CancellationToken token, SubTaskPutModel task, string id, string userId)
    {
        var oldTask = await _taskRepository.GetAsync(token, _hid.Decode(id)).ConfigureAwait(false) ??
            throw new NotFound(ErrorMessages.TaskNotFound);

        var isUsersTodo = await _todoService.ValidateUser(token, _hid.Encode(oldTask.ToDoId), userId).ConfigureAwait(false);
        if (!isUsersTodo)
            throw new NotFound(ErrorMessages.TaskNotFound);

        var newTask = task.Adapt<SubTask>();
        newTask.Id = _hid.Decode(id);
        newTask.ToDoId = oldTask.ToDoId;
        var transformedTask = Transform<SubTask>.Copy(newTask, oldTask);
        await _taskRepository.UpdateAsync(token, transformedTask).ConfigureAwait(false);

        _unitOfWork.SaveChanges();
    }

}
