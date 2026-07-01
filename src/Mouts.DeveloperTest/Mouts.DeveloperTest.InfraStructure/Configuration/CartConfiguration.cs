using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mouts.DeveloperTest.Domain;
using Mouts.DeveloperTest.Domain.ValueObjects;

namespace Mouts.DeveloperTest.InfraStructure.Configuration
{
    public sealed class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Carts");

            builder.HasKey(x => x.CartId);

            builder.Property(x => x.CartId)
                .HasConversion(
                    id => id.Value,
                    value => new CartId(value))
                .ValueGeneratedOnAdd();

            builder.Property(x => x.UserId)
                .HasConversion(
                    id => id.Value,
                    value => new UserId(value))
                .IsRequired();

            builder.Property(x => x.Date)
                .IsRequired();

            builder.OwnsMany(x => x.Products, items =>
            {
                items.ToTable("CartItems");

                items.WithOwner().HasForeignKey("CartId");

                items.Property<int>("Id");

                items.HasKey("Id");

                items.Property(x => x.ProductId)
                    .HasConversion(
                        id => id.Value,
                        value => new ProductId(value))
                    .IsRequired();

                items.Property(x => x.Quantity)
                    .IsRequired();
            });
        }
    }
}
