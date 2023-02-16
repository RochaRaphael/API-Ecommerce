using API_Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_Ecommerce.Data.Mapping
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("VARCHAR")
                .HasMaxLength(256);

            builder.Property(x => x.Url)
                .IsRequired()
                .HasColumnName("Url")
                .HasColumnType("VARCHAR")
                .HasMaxLength(256);

            builder.Property(x => x.Quantity)
                .IsRequired()
                .HasColumnName("Quantity")
                .HasColumnType("INT");

            builder.Property(x => x.Active)
                .IsRequired()
                .HasColumnName("Active")
                .HasColumnType("BIT");

            builder.Property(x => x.Deleted)
                .IsRequired()
                .HasColumnName("Deleted")
                .HasColumnType("BIT");

            builder
                .HasOne(x => x.Category)
                .WithMany(x => x.Products)
                .HasConstraintName("FK_Product_Category");

        }
    }
}
