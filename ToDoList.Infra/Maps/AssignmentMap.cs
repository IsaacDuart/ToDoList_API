using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Domain.Entities;

namespace ToDoList.Infra.Maps;

public class AssignmentMap : IEntityTypeConfiguration<Assignment>
{
    public void Configure(EntityTypeBuilder<Assignment> builder)
    {
        builder.ToTable("Assignments");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.AssignmentListId)
            .HasColumnName("assignment_list_id")
            .HasColumnType("BIGINT");
        
        builder.Property(x => x.UserId)
            .IsRequired()
            .HasColumnName("user_id")
            .HasColumnType("BIGINT");
        
        builder.Property(x => x.Id)
            .UseMySqlIdentityColumn()
            .HasColumnName("id")
            .HasColumnType("BIGINT");
        
        builder.Property(x => x.Description)
            .IsRequired()
            .HasColumnName("description")
            .HasColumnType("TEXT");

        builder.Property(x => x.AssignmentListId)
            .HasColumnName("assignment_list_id")
            .IsRequired();
        
        builder.Property(x => x.Conclued)
            .IsRequired()
            .HasDefaultValue(false)
            .HasColumnName("conclued")
            .HasColumnType("TINYINT(1)"); //verificar se ta certo

        builder.Property(x => x.ConcluedAt)
            .HasColumnName("conclued_at")
            .HasColumnType("DATETIME");
        
        builder.Property(x => x.DeadLine)
            .HasColumnName("dead_line")
            .IsRequired()
            .HasColumnType("DATETIME");

        builder.HasOne(x => x.AssignmentList)
            .WithMany(x => x.Assignments)
            .HasForeignKey(x => x.AssignmentListId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.User)
            .WithMany(x => x.Assignments)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        

    }
}