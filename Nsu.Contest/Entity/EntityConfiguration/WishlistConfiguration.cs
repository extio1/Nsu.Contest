namespace Nsu.Contest.Entity.EntityConfiguration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class WishlistConfiguration : IEntityTypeConfiguration<Wishlist>
{
    public void Configure(EntityTypeBuilder<Wishlist> builder)
    {
        builder.ToTable("Wishlist");

        builder.HasKey(w => w.Id);

        builder.Property(w => w.Id)
               .HasColumnName("Id");

        builder.HasOne(w => w.ForEmployee)
               .WithMany()
               .HasForeignKey(w => w.ForEmployeeId);

    }
}

