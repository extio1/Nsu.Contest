namespace Nsu.Contest.Entity.EntityConfiguration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class JuniorConfiguration: EmployeeConfiguration<Junior>
{
    public override void Configure(EntityTypeBuilder<Junior> builder)
    {
       base.Configure(builder);
       builder.ToTable("Junior");
    }
}
