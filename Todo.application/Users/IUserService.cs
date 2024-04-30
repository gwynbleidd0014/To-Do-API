using Todo.application.Users.Request;
using Todo.application.Users.Response;

namespace Todo.application.Users;

public interface IUserService
{
    Task<UserResponseModel> GetAsync(CancellationToken token, string id);
    Task AddAsync(CancellationToken token, UserRequestModel model);
    Task RemoveAsync(CancellationToken token, string id);
    Task<UserResponseModel> LogIn(CancellationToken token, UserRequestModel user);
    Task Register(CancellationToken token, UserRequestModel user);
    Task Update(CancellationToken token, UserUpdateModel user, string id);
}
