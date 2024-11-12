using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Domain.Entities;

namespace ToDoList.Infra.Maps;

public class AssignmentListMap : IEntityTypeConfiguration<AssignmentList>
{
    public void Configure(EntityTypeBuilder<AssignmentList> builder)
    {
        builder.ToTable("AssignmentList");
        builder.HasKey(x => x.Id);


        builder.Property(x => x.UserId)
            .IsRequired()
            .HasColumnName("user_id")
            .HasColumnType("BIGINT");
        
        builder.Property(x => x.Id)
            .UseMySqlIdentityColumn()
            .HasColumnType("BIGINT")
            .HasColumnName("id");

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnName("name")
            .HasColumnType("VARCHAR(100)");
        
        
        builder.HasMany(x => x.Assignments)
            .WithOne(x => x.AssignmentList)
            .HasForeignKey(x => x.AssignmentListId)
            .OnDelete(DeleteBehavior.Cascade);
            
    }
}