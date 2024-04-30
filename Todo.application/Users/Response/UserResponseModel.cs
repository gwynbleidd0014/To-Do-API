using Todo.application.Todos.Response;

namespace Todo.application.Users.Response;

public class UserResponseModel
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public List<TodoResponseModel> Todos { get; set; }
}
