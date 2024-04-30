using Mapster;
using Todo.application.Abstractions;
using Todo.application.Exceptions.CustomExceptions;
using Todo.application.Exceptions.ErrorMessages;
using Todo.application.Helpers;
using Todo.application.Todos.Request;
using Todo.application.Todos.Response;
using Todo.application.Users;
using Todo.domain;
using Todo.domain.Todos;

namespace Todo.application.Todos;

public class TodoService : ITodoService
{
    private readonly ITodoRepository _todoRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHid _hid;
    private readonly IUserService _userService;

    public TodoService(ITodoRepository todoRepository, IUnitOfWork unitOfWork, IHid hid, IUserService userService)
    {
        _todoRepository = todoRepository;
        _unitOfWork = unitOfWork;
        _hid = hid;
        _userService = userService;
    }

    public async Task AddAsync(CancellationToken token, TodoPutModel todo, string ownerId)
    {
        var transformedTodo = todo.Adapt<ToDo>();
        transformedTodo.OwnerId = _hid.Decode(ownerId);
        await _todoRepository.AddAsync(token, transformedTodo).ConfigureAwait(false);

        _unitOfWork.SaveChanges();
    }

    public async Task<List<TodoResponseModel>> GetAllAsync(CancellationToken token, string? status)
    {
        if (status != "Active" && status != "Done" && status is not null)
            throw new InvalidStatus(ErrorMessages.InvalidStatus);

        EntityStatus? enumStatus;
        if (status is null)
            enumStatus = null;
        else
            enumStatus = EntityStatus.Active.ToString() == status ? EntityStatus.Active : EntityStatus.Done;

        var todos = await _todoRepository
            .GetAllAsync(token, enumStatus).ConfigureAwait(false);

        return todos.Adapt<List<TodoResponseModel>>();
    }

    public async Task<TodoResponseModel> GetAsync(CancellationToken token, string id, string userId)
    {
        var todo = await _todoRepository.GetAsync(token, _hid.Decode(id)).ConfigureAwait(false) ??
            throw new NotFound(ErrorMessages.TodoNotFound);

        var isUsersTodo = await ValidateUser(token, _hid.Encode(todo.Id), userId).ConfigureAwait(false);
        if (!isUsersTodo)
            throw new NotFound(ErrorMessages.TodoNotFound);

        return todo.Adapt<TodoResponseModel>();
    }

    public async Task RemoveAsync(CancellationToken token, string id, string userId)
    {
        _ = await _todoRepository.GetAsync(token, _hid.Decode(id)).ConfigureAwait(false) ?? throw new NotFound(ErrorMessages.TodoNotFound);

        var isUsersTodo = await ValidateUser(token, id, userId).ConfigureAwait(false);
        if (!isUsersTodo)
            throw new NotFound(ErrorMessages.TodoNotFound);

        await _todoRepository.RemoveAsync(token, _hid.Decode(id)).ConfigureAwait(false);

        _unitOfWork.SaveChanges();
    }

    public async Task UpdateAsync<T>(CancellationToken token, T todo, string todoId, string userId)
    {
        if (todo is null)
            throw new EmptyNotAllowed(ErrorMessages.EmptyObject);

        var isUsersTodo = await ValidateUser(token, todoId, userId).ConfigureAwait(false);
        if (!isUsersTodo)
            throw new NotFound(ErrorMessages.TodoNotFound);

        var oldTodo = await _todoRepository.GetAsync(token, _hid.Decode(todoId)).ConfigureAwait(false) ??
            throw new NotFound(ErrorMessages.TodoNotFound);

        var newTodo = todo.Adapt<ToDo>();
        newTodo.Id = _hid.Decode(todoId);
        newTodo.OwnerId = _hid.Decode(userId);
        var transformedTodo = Transform<ToDo>.Copy(newTodo, oldTodo);
        await _todoRepository.UpdateAsync(token, transformedTodo).ConfigureAwait(false);

        _unitOfWork.SaveChanges();
    }

    public async Task UpdateStatusAsync(CancellationToken token, string id, string userId)
    {
        var oldTodo = await _todoRepository.GetAsync(token, _hid.Decode(id)).ConfigureAwait(false) ?? throw new NotFound(ErrorMessages.TodoNotFound);


        var isUsersTodo = await ValidateUser(token, id, userId).ConfigureAwait(false);
        if (!isUsersTodo)
            throw new NotFound(ErrorMessages.TodoNotFound);

        oldTodo.Status = EntityStatus.Done;
        await _todoRepository.UpdateAsync(token, oldTodo).ConfigureAwait(false);

        _unitOfWork.SaveChanges();
    }

    public async Task<bool> ValidateUser(CancellationToken token, string todoId, string userId)
    {
        var user = await _userService.GetAsync(token, userId).ConfigureAwait(false);

        foreach (var todo in user.Todos)
        {
            if (todo.Id == todoId)
                return true;
        }

        return false;
    }
}
