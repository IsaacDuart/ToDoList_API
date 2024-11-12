using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Domain.Entities;

namespace ToDoList.Infra.Maps;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .UseMySqlIdentityColumn()
            .HasColumnType("BIGINT")
            .HasColumnName("id");

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnName("name")
            .HasColumnType("varchar(100)")
            .HasMaxLength(100);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasColumnName("email")
            .HasColumnType("varchar(180)")
            .HasMaxLength(180);
        
        builder.Property(x => x.Password)
            .IsRequired()
            .HasColumnName("password")
            .HasColumnType("varchar(180)")
            .HasMaxLength(180);

        builder.HasMany(x => x.AssignmentLists)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(x => x.Assignments)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);
    }
}