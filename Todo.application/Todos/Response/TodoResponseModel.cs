using Todo.application.SubTasks.Response;

namespace Todo.application.Todos.Response;

public class TodoResponseModel
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string OwnerId { get; set; }
    public DateTime ComplitionDate { get; set; }
    public string Status { get; set; }
    public List<SubTaskResponseModel> SubTasks { get; set; }
}
