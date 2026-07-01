using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mouts.DeveloperTest.Domain;
using Mouts.DeveloperTest.Domain.ValueObjects;

namespace Mouts.DeveloperTest.InfraStructure.Configuration
{
    public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(x => x.ProductId);

            builder.Property(x => x.ProductId)
                .HasConversion(
                    id => id.Value,
                    value => new ProductId(value))
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Title)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.Price)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(4000)
                .IsRequired();

            builder.Property(x => x.Category)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Image)
                .HasMaxLength(500)
                .IsRequired();

            builder.OwnsOne(x => x.Rating, rating =>
            {
                rating.Property(x => x.Rate)
                    .HasColumnName("Rate")
                    .HasPrecision(4, 2)
                    .IsRequired();

                rating.Property(x => x.Count)
                    .HasColumnName("Count")
                    .IsRequired();
            });

            builder.HasIndex(x => x.Category);

            builder.HasIndex(x => x.Title);
        }
    }
}
