using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mouts.DeveloperTest.Domain;

namespace Mouts.DeveloperTest.InfraStructure.Configuration
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.UserId);

            builder.Property(x => x.UserId)
                .HasConversion(
                    id => id.Value,
                    value => new UserId(value))
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Email)
                .HasConversion(
                    email => email.Value,
                    value => new Email(value))
                .HasMaxLength(200)
                .IsRequired();

            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.Property(x => x.UserName)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasIndex(x => x.UserName)
                .IsUnique();

            builder.Property(x => x.PasswordHash)
                .HasConversion(
                    hash => hash.Value,
                    value => new PasswordHash(value))
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(x => x.Phone)
                .HasConversion(
                    phone => phone.Value,
                    value => new Phone(value))
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(x => x.Status)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.Role)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

            builder.OwnsOne(x => x.Name, name =>
            {
                name.Property(x => x.FirstName)
                    .HasColumnName("FirstName")
                    .HasMaxLength(100)
                    .IsRequired();

                name.Property(x => x.LastName)
                    .HasColumnName("LastName")
                    .HasMaxLength(100)
                    .IsRequired();
            });

            builder.OwnsOne(x => x.Address, address =>
            {
                address.Property(x => x.City)
                    .HasColumnName("City")
                    .HasMaxLength(100)
                    .IsRequired();

                address.Property(x => x.Street)
                    .HasColumnName("Street")
                    .HasMaxLength(200)
                    .IsRequired();

                address.Property(x => x.Number)
                    .HasColumnName("Number")
                    .IsRequired();

                address.Property(x => x.ZipCode)
                    .HasColumnName("ZipCode")
                    .HasMaxLength(20)
                    .IsRequired();

                address.OwnsOne(x => x.GeoLocation, geo =>
                {
                    geo.Property(x => x.Latitude)
                        .HasColumnName("Latitude")
                        .HasMaxLength(50)
                        .IsRequired();

                    geo.Property(x => x.Longitude)
                        .HasColumnName("Longitude")
                        .HasMaxLength(50)
                        .IsRequired();
                });
            });
        }
    }
}
