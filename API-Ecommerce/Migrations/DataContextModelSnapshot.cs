﻿// <auto-generated />
using System;
using API_Ecommerce.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API_Ecommerce.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("API_Ecommerce.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("BIT")
                        .HasColumnName("Active");

                    b.Property<bool>("Deleted")
                        .HasColumnType("BIT")
                        .HasColumnName("Deleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Name");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Url");

                    b.HasKey("Id");

                    b.ToTable("Category", (string)null);
                });

            modelBuilder.Entity("API_Ecommerce.Models.ItemOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("ItemOrder", (string)null);
                });

            modelBuilder.Entity("API_Ecommerce.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("OrderDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DATE")
                        .HasDefaultValue(new DateTime(2023, 3, 15, 17, 43, 20, 886, DateTimeKind.Utc).AddTicks(7534))
                        .HasColumnName("OrderDate");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Order", (string)null);
                });

            modelBuilder.Entity("API_Ecommerce.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("BIT")
                        .HasColumnName("Active");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<bool>("Deleted")
                        .HasColumnType("BIT")
                        .HasColumnName("Deleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Name");

                    b.Property<int>("Quantity")
                        .HasColumnType("INT")
                        .HasColumnName("Quantity");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Url");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("API_Ecommerce.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Name");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Slug");

                    b.HasKey("Id");

                    b.ToTable("Role", (string)null);
                });

            modelBuilder.Entity("API_Ecommerce.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("BIT")
                        .HasColumnName("Ativo");

                    b.Property<bool>("Deleted")
                        .HasColumnType("BIT")
                        .HasColumnName("Deleted");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Email");

                    b.Property<bool?>("IsChecked")
                        .HasColumnType("BIT")
                        .HasColumnName("IsChecked");

                    b.Property<string>("LastToken")
                        .HasMaxLength(256)
                        .HasColumnType("NVARCHAR(MAX)")
                        .HasColumnName("LastToken");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Login");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Password");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(MAX)")
                        .HasColumnName("Salt");

                    b.Property<string>("VerificationKey")
                        .HasMaxLength(256)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("VerificationKey");

                    b.HasKey("Id");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("UserRole", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("RoleId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("API_Ecommerce.Models.ItemOrder", b =>
                {
                    b.HasOne("API_Ecommerce.Models.Order", "Order")
                        .WithMany("ItemOrders")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_ItemOrder_Order");

                    b.HasOne("API_Ecommerce.Models.Product", "Product")
                        .WithMany("ItemOrders")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_ItemOrder_Product");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("API_Ecommerce.Models.Order", b =>
                {
                    b.HasOne("API_Ecommerce.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Order_User");

                    b.Navigation("User");
                });

            modelBuilder.Entity("API_Ecommerce.Models.Product", b =>
                {
                    b.HasOne("API_Ecommerce.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Product_Category");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("UserRole", b =>
                {
                    b.HasOne("API_Ecommerce.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_UserRole_RoleId");

                    b.HasOne("API_Ecommerce.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_UserRole_UserId");
                });

            modelBuilder.Entity("API_Ecommerce.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("API_Ecommerce.Models.Order", b =>
                {
                    b.Navigation("ItemOrders");
                });

            modelBuilder.Entity("API_Ecommerce.Models.Product", b =>
                {
                    b.Navigation("ItemOrders");
                });

            modelBuilder.Entity("API_Ecommerce.Models.User", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
