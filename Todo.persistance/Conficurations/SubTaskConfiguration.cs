using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.domain;
using Todo.domain.SubTasks;

namespace Todo.persistance.Conficurations;

public class SubTaskConfiguration : IEntityTypeConfiguration<SubTask>
{
    public void Configure(EntityTypeBuilder<SubTask> builder)
    {
        builder.Property(x => x.Title).IsUnicode(false).IsRequired().HasMaxLength(100);
        builder.HasOne(x => x.ToDo).WithMany(x => x.SubTasks).HasForeignKey(x => x.ToDoId);
        builder.HasQueryFilter(x => x.Status != EntityStatus.Deleted);
        builder.Property(x => x.Status).HasConversion<int>();
    }
}
