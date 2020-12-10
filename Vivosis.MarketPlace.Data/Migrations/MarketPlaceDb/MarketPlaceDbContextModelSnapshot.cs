﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Vivosis.MarketPlace.Data;

namespace Vivosis.MarketPlace.Data.Migrations.MarketPlaceDb
{
    [DbContext(typeof(MarketPlaceDbContext))]
    partial class MarketPlaceDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Vivosis.MarketPlace.Data.Entities.Category", b =>
                {
                    b.Property<int>("category_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("date_added")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("date_modified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("status")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("category_id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Vivosis.MarketPlace.Data.Entities.Option", b =>
                {
                    b.Property<int>("option_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("sort_order")
                        .HasColumnType("int");

                    b.Property<string>("type")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("option_id");

                    b.ToTable("Options");
                });

            modelBuilder.Entity("Vivosis.MarketPlace.Data.Entities.OptionValue", b =>
                {
                    b.Property<int>("option_value_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("option_id")
                        .HasColumnType("int");

                    b.Property<int>("sort_order")
                        .HasColumnType("int");

                    b.HasKey("option_value_id");

                    b.HasIndex("option_id");

                    b.ToTable("OptionValues");
                });

            modelBuilder.Entity("Vivosis.MarketPlace.Data.Entities.Product", b =>
                {
                    b.Property<int>("product_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("date_added")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("date_modified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("image_url")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<decimal>("price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("quantity")
                        .HasColumnType("decimal(65,30)");

                    b.Property<bool>("status")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("product_id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Vivosis.MarketPlace.Data.Entities.ProductCategory", b =>
                {
                    b.Property<int>("product_id")
                        .HasColumnType("int");

                    b.Property<int>("category_id")
                        .HasColumnType("int");

                    b.HasKey("product_id", "category_id");

                    b.HasIndex("category_id");

                    b.ToTable("ProductCategory");
                });

            modelBuilder.Entity("Vivosis.MarketPlace.Data.Entities.ProductOption", b =>
                {
                    b.Property<int>("product_option_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("is_required")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("option_id")
                        .HasColumnType("int");

                    b.Property<int>("product_id")
                        .HasColumnType("int");

                    b.Property<string>("value")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("product_option_id");

                    b.HasIndex("option_id");

                    b.HasIndex("product_id");

                    b.ToTable("ProductOptions");
                });

            modelBuilder.Entity("Vivosis.MarketPlace.Data.Entities.ProductOptionValue", b =>
                {
                    b.Property<int>("product_option_value_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("option_value_id")
                        .HasColumnType("int");

                    b.Property<int>("point")
                        .HasColumnType("int");

                    b.Property<decimal>("price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("product_option_id")
                        .HasColumnType("int");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("weight")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("product_option_value_id");

                    b.HasIndex("option_value_id");

                    b.HasIndex("product_option_id", "option_value_id")
                        .IsUnique();

                    b.ToTable("ProductOptionValues");
                });

            modelBuilder.Entity("Vivosis.MarketPlace.Data.Entities.Store", b =>
                {
                    b.Property<int>("store_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("image")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ssl")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("url")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("store_id");

                    b.ToTable("Stores");
                });

            modelBuilder.Entity("Vivosis.MarketPlace.Data.Entities.StoreCategory", b =>
                {
                    b.Property<int>("store_id")
                        .HasColumnType("int");

                    b.Property<int>("category_id")
                        .HasColumnType("int");

                    b.Property<int>("commission")
                        .HasColumnType("int");

                    b.Property<string>("currency")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("matched_category")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<decimal>("shipping_fee")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("store_id", "category_id");

                    b.HasIndex("category_id");

                    b.ToTable("StoreCategory");
                });

            modelBuilder.Entity("Vivosis.MarketPlace.Data.Entities.StoreProduct", b =>
                {
                    b.Property<int>("store_id")
                        .HasColumnType("int");

                    b.Property<int>("product_id")
                        .HasColumnType("int");

                    b.Property<int>("commission")
                        .HasColumnType("int");

                    b.Property<string>("currency")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("is_active")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("matched_category")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("origin")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<decimal>("sale_price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("shipping_fee")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("strikethrough_price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("url")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("store_id", "product_id");

                    b.HasIndex("product_id");

                    b.ToTable("StoreProduct");
                });

            modelBuilder.Entity("Vivosis.MarketPlace.Data.Entities.StoreUser", b =>
                {
                    b.Property<int>("store_id")
                        .HasColumnType("int");

                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.Property<string>("api_key")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<DateTime>("expire_time")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("is_active")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("is_confirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("secret_key")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("store_id", "user_id");

                    b.HasIndex("api_key")
                        .IsUnique();

                    b.HasIndex("user_id");

                    b.ToTable("StoreUser");
                });

            modelBuilder.Entity("Vivosis.MarketPlace.Data.Entities.SystemUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("DbName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("DbPassword")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("DbUserName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Email")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("ExpireTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("FullName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Server")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("SystemUser");
                });

            modelBuilder.Entity("Vivosis.MarketPlace.Data.Entities.OptionValue", b =>
                {
                    b.HasOne("Vivosis.MarketPlace.Data.Entities.Option", "Option")
                        .WithMany("OptionValues")
                        .HasForeignKey("option_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Vivosis.MarketPlace.Data.Entities.ProductCategory", b =>
                {
                    b.HasOne("Vivosis.MarketPlace.Data.Entities.Category", "Category")
                        .WithMany("CategoryProducts")
                        .HasForeignKey("category_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Vivosis.MarketPlace.Data.Entities.Product", "Product")
                        .WithMany("ProductCategories")
                        .HasForeignKey("product_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Vivosis.MarketPlace.Data.Entities.ProductOption", b =>
                {
                    b.HasOne("Vivosis.MarketPlace.Data.Entities.Option", "Option")
                        .WithMany("ProductOptions")
                        .HasForeignKey("option_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Vivosis.MarketPlace.Data.Entities.Product", "Product")
                        .WithMany("ProductOptions")
                        .HasForeignKey("product_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Vivosis.MarketPlace.Data.Entities.ProductOptionValue", b =>
                {
                    b.HasOne("Vivosis.MarketPlace.Data.Entities.OptionValue", "OptionValue")
                        .WithMany("ProductOptionValues")
                        .HasForeignKey("option_value_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Vivosis.MarketPlace.Data.Entities.ProductOption", "ProductOption")
                        .WithMany("ProductOptionValues")
                        .HasForeignKey("product_option_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Vivosis.MarketPlace.Data.Entities.StoreCategory", b =>
                {
                    b.HasOne("Vivosis.MarketPlace.Data.Entities.Category", "Category")
                        .WithMany("CategoryStores")
                        .HasForeignKey("category_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Vivosis.MarketPlace.Data.Entities.Store", "Store")
                        .WithMany("StoreCategories")
                        .HasForeignKey("store_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Vivosis.MarketPlace.Data.Entities.StoreProduct", b =>
                {
                    b.HasOne("Vivosis.MarketPlace.Data.Entities.Product", "Product")
                        .WithMany("ProductStores")
                        .HasForeignKey("product_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Vivosis.MarketPlace.Data.Entities.Store", "Store")
                        .WithMany("StoreProducts")
                        .HasForeignKey("store_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Vivosis.MarketPlace.Data.Entities.StoreUser", b =>
                {
                    b.HasOne("Vivosis.MarketPlace.Data.Entities.Store", "Store")
                        .WithMany()
                        .HasForeignKey("store_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Vivosis.MarketPlace.Data.Entities.SystemUser", "User")
                        .WithMany("UserStores")
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
