using Todo.domain.SubTasks;
using Todo.domain.Users;

namespace Todo.domain.Todos;

public class ToDo : BaseEntity, ISoftDelete
{
    public string Title { get; set; }
    public DateTime? ComplitionDate { get; set; }
    public int OwnerId { get; set; }
    public User User { get; set; }
    public List<SubTask> SubTasks { get; set; }
}
