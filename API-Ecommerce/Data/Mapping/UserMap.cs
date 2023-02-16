using API_Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_Ecommerce.Data.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Usuario");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("VARCHAR")
                .HasMaxLength(256);

            builder.Property(x => x.Login)
                .IsRequired()
                .HasColumnName("Login")
                .HasColumnType("VARCHAR")
                .HasMaxLength(256);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasColumnName("Email")
                .HasColumnType("VARCHAR")
                .HasMaxLength(256);

            builder.Property(x => x.Password)
                .IsRequired()
                .HasColumnName("Password")
                .HasColumnType("VARCHAR")
                .HasMaxLength(256);

            builder.Property(x => x.VerificationKey)
                .HasColumnName("VerificationKey")
                .HasColumnType("VARCHAR")
                .HasMaxLength(256);

            builder.Property(x => x.LastToken)
                .HasColumnName("LastToken")
                .HasColumnType("NVARCHAR(MAX)")
                .HasMaxLength(256);

            builder.Property(x => x.IsChecked)
                .HasColumnName("IsVerification")
                .HasColumnType("BIT");

            builder.Property(x => x.Active)
                .HasColumnName("Ativo")
                .HasColumnType("BIT");

            builder.Property(x => x.Deleted)
                .HasColumnName("Excluido")
                .HasColumnType("BIT");
        }
    }
}
