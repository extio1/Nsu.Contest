namespace Nsu.Contest.Entity.EntityConfiguration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public virtual void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employees");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).HasColumnName("Id");
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        
        builder.Property(e => e.Name)
               .HasColumnName("Name")
               .HasMaxLength(250)
               .IsRequired();

        builder.HasDiscriminator<string>("EmployeeType")
               .HasValue<Employee>("Employee")
               .HasValue<Junior>("Junior")
               .HasValue<Teamlead>("Teamlead");

        builder.HasDiscriminator().IsComplete();
    }
}

