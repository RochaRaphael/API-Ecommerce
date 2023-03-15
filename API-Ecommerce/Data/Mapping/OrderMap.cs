using API_Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_Ecommerce.Data.Mapping
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.OrderDate)
                .IsRequired()
                .HasColumnName("OrderDate")
                .HasColumnType("DATE")
                .HasDefaultValue(DateTime.Now.ToUniversalTime());

            builder
                .HasOne(x => x.User)
                .WithMany(x => x.Orders)
                .HasConstraintName("FK_Order_User");

            
        }
    }
}
