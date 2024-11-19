namespace Nsu.Contest.Entity.EntityConfiguration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class TeamleadConfiguration: EmployeeConfiguration<Teamlead>
{
    public override void Configure(EntityTypeBuilder<Teamlead> builder)
    {
        base.Configure(builder);
        builder.ToTable("Teamlead");
    }
}
