using API_Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_Ecommerce.Data.Mapping
{
    public class ItemOrderMap : IEntityTypeConfiguration<ItemOrder>
    {
        public void Configure(EntityTypeBuilder<ItemOrder> builder)
        {
            builder.ToTable("ItemOrder");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder
                .HasOne(x => x.Order)
                .WithMany(x => x.ItemOrders)
                .HasConstraintName("FK_ItemOrder_Order");
            builder
                .HasOne(x => x.Product)
                .WithMany(x => x.ItemOrders)
                .HasConstraintName("FK_ItemOrder_Product");
        }
    }
}
