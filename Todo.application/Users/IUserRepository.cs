using Todo.application.Users.Request;
using Todo.domain.Users;

namespace Todo.application.Users;

public interface IUserRepository
{
    Task<User?> GetByUserNameAsync(CancellationToken token, string userName);
    Task<User?> GetAsync(CancellationToken token, int id);
    Task AddAsync(CancellationToken token, User user);
    Task<bool> RemoveAsync(CancellationToken token, int id);
    Task Update(User user);
    Task<User?> GetFullAsync(CancellationToken token, int id);
}
