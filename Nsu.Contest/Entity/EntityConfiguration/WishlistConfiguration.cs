namespace Nsu.Contest.Entity.EntityConfiguration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class WishlistConfiguration: IEntityTypeConfiguration<Wishlist>
{
    public void Configure(EntityTypeBuilder<Wishlist> builder)
    {
        builder.ToTable("Team");

        builder.HasKey(t => t.Id);
        
        builder.Property(t => t.Id)
                .HasColumnName("Id");

        builder.HasOne(t => t.ForEmployee).WithMany();
        builder.HasMany(t => t.DesiredEmployees).WithMany();
    }
}
