using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.domain.Users;
using Todo.domain;

namespace Todo.persistance.Conficurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(x => x.UserName).IsUnique();
        builder.Property(x => x.UserName).IsUnicode(false).IsRequired().HasMaxLength(50);
        builder.Property(x => x.PasswordHash).IsUnicode(false).IsRequired().HasMaxLength(200);
        builder.HasQueryFilter(x => x.Status != EntityStatus.Deleted);
        builder.Property(x => x.Status).HasConversion<int>();
    }
}
