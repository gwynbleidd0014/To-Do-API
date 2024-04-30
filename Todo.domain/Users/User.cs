using Todo.domain.Todos;

namespace Todo.domain.Users
{
    public class User : BaseEntity, ISoftDelete
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public List<ToDo> Todos { get; set; }
    }
}
