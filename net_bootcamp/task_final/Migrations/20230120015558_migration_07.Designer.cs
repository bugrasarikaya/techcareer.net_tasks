﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using task_final.Models;

#nullable disable

namespace taskfinal.Migrations
{
    [DbContext(typeof(ShoppingListDbContext))]
    [Migration("20230120015558_migration_07")]
    partial class migration07
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("task_final.Models.Account", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar");

                    b.HasKey("ID");

                    b.ToTable("Accounts", (string)null);
                });

            modelBuilder.Entity("task_final.Models.Image", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<byte[]>("Binary")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar");

                    b.HasKey("ID");

                    b.ToTable("Images", (string)null);
                });

            modelBuilder.Entity("task_final.Models.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar");

                    b.Property<string>("Description")
                        .HasMaxLength(20)
                        .HasColumnType("varchar");

                    b.Property<int>("ImageID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar");

                    b.Property<double?>("Price")
                        .HasColumnType("float");

                    b.HasKey("ID");

                    b.HasIndex("ImageID")
                        .IsUnique();

                    b.ToTable("Products", (string)null);
                });

            modelBuilder.Entity("task_final.Models.ShoppingList", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar");

                    b.Property<int?>("ShoppingProductListID")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar");

                    b.HasKey("ID");

                    b.ToTable("ShoppingLists", (string)null);
                });

            modelBuilder.Entity("task_final.Models.ShoppingProduct", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.Property<int>("ShoppingProductListID")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar");

                    b.HasKey("ID");

                    b.HasIndex("ProductID");

                    b.HasIndex("ShoppingProductListID");

                    b.ToTable("ShoppingProducts", (string)null);
                });

            modelBuilder.Entity("task_final.Models.ShoppingProductList", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("ShoppingListID")
                        .HasColumnType("int");

                    b.Property<int>("ShoppingProductID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ShoppingListID")
                        .IsUnique();

                    b.ToTable("ShoppingProductLists", (string)null);
                });

            modelBuilder.Entity("task_final.Models.Product", b =>
                {
                    b.HasOne("task_final.Models.Image", "Image")
                        .WithOne("Product")
                        .HasForeignKey("task_final.Models.Product", "ImageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Image");
                });

            modelBuilder.Entity("task_final.Models.ShoppingProduct", b =>
                {
                    b.HasOne("task_final.Models.Product", "Product")
                        .WithMany("ShoppingProducts")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("task_final.Models.ShoppingProductList", "ShoppingProductList")
                        .WithMany("ShoppingProducts")
                        .HasForeignKey("ShoppingProductListID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("ShoppingProductList");
                });

            modelBuilder.Entity("task_final.Models.ShoppingProductList", b =>
                {
                    b.HasOne("task_final.Models.ShoppingList", "ShoppingList")
                        .WithOne("ShoppingProductList")
                        .HasForeignKey("task_final.Models.ShoppingProductList", "ShoppingListID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ShoppingList");
                });

            modelBuilder.Entity("task_final.Models.Image", b =>
                {
                    b.Navigation("Product")
                        .IsRequired();
                });

            modelBuilder.Entity("task_final.Models.Product", b =>
                {
                    b.Navigation("ShoppingProducts");
                });

            modelBuilder.Entity("task_final.Models.ShoppingList", b =>
                {
                    b.Navigation("ShoppingProductList");
                });

            modelBuilder.Entity("task_final.Models.ShoppingProductList", b =>
                {
                    b.Navigation("ShoppingProducts");
                });
#pragma warning restore 612, 618
        }
    }
}
