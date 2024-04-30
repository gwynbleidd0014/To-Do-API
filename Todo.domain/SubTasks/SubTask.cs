using Todo.domain.Todos;

namespace Todo.domain.SubTasks;

public class SubTask : BaseEntity, ISoftDelete
{
    public string Title { get; set; }
    public int ToDoId { get; set; }
    public ToDo ToDo { get; set; }
}
