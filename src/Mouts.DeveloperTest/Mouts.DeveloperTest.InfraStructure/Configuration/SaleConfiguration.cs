using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mouts.DeveloperTest.Domain;
using Mouts.DeveloperTest.Domain.ValueObjects;


namespace Mouts.DeveloperTest.InfraStructure.Configuration
{
    public sealed class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sales");

            builder.HasKey(x => x.SaleId);

            builder.Property(x => x.SaleId)
                .HasConversion(
                    id => id.Value,
                    value => new SaleId(value))
                .ValueGeneratedOnAdd();

            builder.Property(x => x.SaleNumber)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.SaleDate)
                .IsRequired();

            builder.Property(x => x.Cancelled)
                .IsRequired();

            #region Customer (External Identity)

            builder.OwnsOne(x => x.Customer, c =>
            {
                c.Property(x => x.CustomerId)
                    .HasColumnName("CustomerId");

                c.Property(x => x.CustomerName)
                    .HasColumnName("CustomerName")
                    .HasMaxLength(200);
            });

            #endregion

            #region Branch

            builder.OwnsOne(x => x.Branch, b =>
            {
                b.Property(x => x.BranchId)
                    .HasColumnName("BranchId");

                b.Property(x => x.BranchName)
                    .HasColumnName("BranchName")
                    .HasMaxLength(200);
            });

            #endregion

            #region Items

            builder.OwnsMany(x => x.Items, items =>
            {
                items.ToTable("SaleItems");

                items.WithOwner().HasForeignKey("SaleId");

                items.Property<int>("Id")
                 .ValueGeneratedOnAdd();

                items.HasKey("Id");

                items.Property(x => x.SaleId)
                    .IsRequired();

                items.OwnsOne(x => x.Product, p =>
                {
                    p.Property(x => x.ProductId)
                        .HasColumnName("ProductId");

                    p.Property(x => x.ProductTitle)
                        .HasColumnName("ProductTitle")
                        .HasMaxLength(200);
                });

                items.Property(x => x.Quantity)
                    .IsRequired();

                items.Property(x => x.UnitPrice)
                    .HasPrecision(18, 2);

                items.Property(x => x.Discount)
                    .HasPrecision(18, 2);

                items.Property(x => x.Cancelled);
            });

            #endregion
        }
    }
}
