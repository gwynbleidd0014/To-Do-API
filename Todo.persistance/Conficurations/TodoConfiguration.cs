using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.domain;
using Todo.domain.Todos;

namespace Todo.persistance.Conficurations;

public class TodoConfiguration : IEntityTypeConfiguration<ToDo>
{
    public void Configure(EntityTypeBuilder<ToDo> builder)
    {
        builder.Property(x => x.Title).IsUnicode(false).IsRequired().HasMaxLength(100);
        builder.HasOne(x => x.User).WithMany(x => x.Todos).HasForeignKey(x => x.OwnerId);
        builder.HasQueryFilter(x => x.Status != EntityStatus.Deleted);
        builder.Property(x => x.Status).HasConversion<int>();
    }
}
