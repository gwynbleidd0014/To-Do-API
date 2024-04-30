using Microsoft.EntityFrameworkCore;
using Todo.domain;
using Todo.domain.SubTasks;
using Todo.domain.Todos;
using Todo.domain.Users;

namespace Todo.persistance.Context;

public class TodoContext : DbContext
{
    public TodoContext(DbContextOptions<TodoContext> options) : base(options)
    {
        
    }

    public DbSet<User>? Users { get; set; }
    public DbSet<ToDo>? Todos { get; set; }
    public DbSet<SubTask>? SubTasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TodoContext).Assembly);
    }

    public override int SaveChanges()
    {
        var entriesForDelete = ChangeTracker.Entries()
            .Where(e => e.Entity is ISoftDelete && e.State == EntityState.Deleted);

        foreach (var entry in entriesForDelete)
        {
            var entity = entry.Entity;
            entry.State = EntityState.Modified;
            ((BaseEntity)entity).Status = EntityStatus.Deleted;
        }

        var entriesForUpdateOrChange = ChangeTracker.Entries()
            .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entry in entriesForUpdateOrChange)
        {
            var entity = entry.Entity;
            ((BaseEntity)entity).ModifiedAt = DateTime.UtcNow;
            if(entry.State == EntityState.Added)
                ((BaseEntity)entity).CreatedAt = DateTime.UtcNow;
        }

        return base.SaveChanges();
    }

}
