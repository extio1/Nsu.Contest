namespace Nsu.Contest.Entity.EntityConfiguration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class EmployeeConfiguration<TEntity>: IEntityTypeConfiguration<TEntity> where TEntity : Employee
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(e => e.Id);
        
        builder.Property(p => p.Id)
            .HasField("Id");
        builder.Property(p => p.Name)
            .HasField("Name")
            .HasMaxLength(250)
            .IsRequired();
    }
}
