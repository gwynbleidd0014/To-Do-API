using Microsoft.EntityFrameworkCore;
using Todo.application.Todos;
using Todo.application.Users;
using Todo.domain.Users;
using Todo.persistance.Context;
using Todo.domain;

namespace Todo.infrastructure.Repositories.Users;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    private readonly ITodoRepository _todoRepository;
    public UserRepository(TodoContext context, ITodoRepository todoRepository) : base(context)
    {
        _todoRepository = todoRepository;
    }

    public async new Task AddAsync(CancellationToken token, User user)
    {
        user.Status = EntityStatus.Active;
        await base.AddAsync(token, user).ConfigureAwait(false);
    }

    public async Task<User?> GetByUserNameAsync(CancellationToken token, string userName)
    {
        var user = await _dbset.SingleOrDefaultAsync(x => x.UserName.Equals(userName), token).ConfigureAwait(false);
        return user;
    }


    public async Task<bool> RemoveAsync(CancellationToken token, int id)
    {
        var user = await GetFullAsync(token, id).ConfigureAwait(false);
        await base.RemoveAsync(token, id).ConfigureAwait(false);

        if (user is null)
            return false;

        foreach (var todo in user.Todos)
        {
            todo.Status = EntityStatus.Deleted;
            await _todoRepository.UpdateAsync(token, todo).ConfigureAwait(false);
        }

        return true;
    }

    public async Task<User?> GetAsync(CancellationToken token, int id)
    {
        return await base.GetAsync(token, id).ConfigureAwait(false);
    }

    public async new Task Update(User user)
    {
        await base.Update(user).ConfigureAwait(false);
    }

    public async Task<User?> GetFullAsync(CancellationToken token, int id)
    {
        var user =  await _dbset
            .Include(x => x.Todos)
            .ThenInclude(x => x.SubTasks)
            .SingleOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);

        return user;
    }

}

