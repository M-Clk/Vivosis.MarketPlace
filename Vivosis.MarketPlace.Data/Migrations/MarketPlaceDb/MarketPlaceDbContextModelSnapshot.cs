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

                    b.Property<string>("path_name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("status")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("category_id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Vivosis.MarketPlace.Data.Entities.CategoryOption", b =>
                {
                    b.Property<int>("category_option_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("is_required")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("matched_store_option_id")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("matched_store_option_name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("option_id")
                        .HasColumnType("int");

                    b.Property<int>("store_category_id")
                        .HasColumnType("int");

                    b.HasKey("category_option_id");

                    b.HasIndex("option_id");

                    b.HasIndex("store_category_id");

                    b.ToTable("CategoryOptions");
                });

            modelBuilder.Entity("Vivosis.MarketPlace.Data.Entities.CategoryOptionValue", b =>
                {
                    b.Property<int>("category_option_value_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("category_option_id")
                        .HasColumnType("int");

                    b.Property<int>("option_value_id")
                        .HasColumnType("int");

                    b.Property<int>("store_category_value_id")
                        .HasColumnType("int");

                    b.Property<string>("store_category_value_name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("category_option_value_id");

                    b.HasIndex("option_value_id");

                    b.HasIndex("category_option_id", "option_value_id")
                        .IsUnique();

                    b.ToTable("CategoryOptionValues");
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
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("quantity")
                        .HasColumnType("decimal(18,2)");

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

                    b.ToTable("productcategory");
                });

            modelBuilder.Entity("Vivosis.MarketPlace.Data.Entities.ProductImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("order")
                        .HasColumnType("int");

                    b.Property<int>("product_id")
                        .HasColumnType("int");

                    b.Property<int?>("product_id1")
                        .HasColumnType("int");

                    b.Property<string>("url")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("product_id1");

                    b.ToTable("ProductImages");
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

                    b.Property<bool>("IsChanged")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("option_value_id")
                        .HasColumnType("int");

                    b.Property<int>("point")
                        .HasColumnType("int");

                    b.Property<decimal>("price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("product_option_id")
                        .HasColumnType("int");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.Property<bool>("subtract")
                        .HasColumnType("tinyint(1)");

                    b.Property<decimal>("weight")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("product_option_value_id");

                    b.HasIndex("option_value_id");

                    b.HasIndex("product_option_id", "option_value_id")
                        .IsUnique();

                    b.ToTable("ProductOptionValues");
                });

            modelBuilder.Entity("Vivosis.MarketPlace.Data.Entities.ShipmentTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("ShipmentTemplates");
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
                    b.Property<int>("store_category_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("category_id")
                        .HasColumnType("int");

                    b.Property<int>("commission")
                        .HasColumnType("int");

                    b.Property<string>("currency")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("is_matched")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("matched_category_code")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("matched_category_name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<decimal>("shipping_fee")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("store_id")
                        .HasColumnType("int");

                    b.HasKey("store_category_id");

                    b.HasIndex("category_id");

                    b.HasIndex("store_id", "category_id")
                        .IsUnique();

                    b.ToTable("storecategory");
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

                    b.Property<bool>("is_sent")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("matched_product_code")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("origin")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<decimal>("sale_price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("shipping_fee")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("strikethrough_price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("url")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("store_id", "product_id");

                    b.HasIndex("product_id");

                    b.ToTable("storeproduct");
                });

            modelBuilder.Entity("Vivosis.MarketPlace.Data.Entities.CategoryOption", b =>
                {
                    b.HasOne("Vivosis.MarketPlace.Data.Entities.Option", "Option")
                        .WithMany("CategoryOptions")
                        .HasForeignKey("option_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Vivosis.MarketPlace.Data.Entities.StoreCategory", "StoreCategory")
                        .WithMany("CategoryOptions")
                        .HasForeignKey("store_category_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Vivosis.MarketPlace.Data.Entities.CategoryOptionValue", b =>
                {
                    b.HasOne("Vivosis.MarketPlace.Data.Entities.CategoryOption", "CategoryOption")
                        .WithMany("CategoryOptionValues")
                        .HasForeignKey("category_option_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Vivosis.MarketPlace.Data.Entities.OptionValue", "OptionValue")
                        .WithMany("CategoryOptionValues")
                        .HasForeignKey("option_value_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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

            modelBuilder.Entity("Vivosis.MarketPlace.Data.Entities.ProductImage", b =>
                {
                    b.HasOne("Vivosis.MarketPlace.Data.Entities.Product", null)
                        .WithMany("ProductImages")
                        .HasForeignKey("product_id1");
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
#pragma warning restore 612, 618
        }
    }
}
