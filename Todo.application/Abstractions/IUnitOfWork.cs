using Todo.persistance.Context;

namespace Todo.application;

public interface IUnitOfWork
{
    public TodoContext Context { get;}
    int SaveChanges();
}
