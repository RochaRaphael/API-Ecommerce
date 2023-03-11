using API_Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_Ecommerce.Data.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

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
                .HasColumnName("IsChecked")
                .HasColumnType("BIT");

            builder.Property(x => x.Active)
                .HasColumnName("Ativo")
                .HasColumnType("BIT");

            builder.Property(x => x.Deleted)
                .HasColumnName("Deleted")
                .HasColumnType("BIT");

            builder.Property(x => x.Salt)
                .HasColumnName("Salt")
                .HasColumnType("NVARCHAR(MAX)");

            builder
                .HasMany(x => x.Roles)
                .WithMany(x => x.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    role => role
                        .HasOne<Role>()
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_UserRole_RoleId")
                        .OnDelete(DeleteBehavior.Cascade),
                    user => user
                        .HasOne<User>()
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_UserRole_UserId")
                        .OnDelete(DeleteBehavior.Cascade));
        }
    }
}
