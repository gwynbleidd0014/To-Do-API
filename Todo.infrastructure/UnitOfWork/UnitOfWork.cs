using Todo.application;
using Todo.persistance.Context;

namespace Todo.infrastructure.UnitOfWork;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly TodoContext _context;
    public UnitOfWork(TodoContext context)
    {
        _context = context;
    }

    public TodoContext Context => _context;

    public int SaveChanges()
    {
        return _context.SaveChanges();
    }

}
