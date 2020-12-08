using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vivosis.MarketPlace.Data.Migrations.MarketPlaceDb
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    category_id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    status = table.Column<bool>(nullable: false),
                    date_added = table.Column<DateTime>(nullable: false),
                    date_modified = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.category_id);
                });

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    option_id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    type = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    sort_order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => x.option_id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    product_id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    status = table.Column<bool>(nullable: false),
                    date_added = table.Column<DateTime>(nullable: false),
                    date_modified = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    image_url = table.Column<string>(nullable: true),
                    price = table.Column<decimal>(nullable: false),
                    quantity = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.product_id);
                });

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    store_id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: true),
                    image = table.Column<string>(nullable: true),
                    url = table.Column<string>(nullable: true),
                    ssl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.store_id);
                });

            migrationBuilder.CreateTable(
                name: "SystemUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ExpireTime = table.Column<DateTime>(nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    FullName = table.Column<string>(nullable: true),
                    DbName = table.Column<string>(nullable: true),
                    DbUserName = table.Column<string>(nullable: true),
                    DbPassword = table.Column<string>(nullable: true),
                    Server = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OptionValues",
                columns: table => new
                {
                    option_value_id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    option_id = table.Column<int>(nullable: false),
                    sort_order = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionValues", x => x.option_value_id);
                    table.ForeignKey(
                        name: "FK_OptionValues_Options_option_id",
                        column: x => x.option_id,
                        principalTable: "Options",
                        principalColumn: "option_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategory",
                columns: table => new
                {
                    product_id = table.Column<int>(nullable: false),
                    category_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory", x => new { x.product_id, x.category_id });
                    table.ForeignKey(
                        name: "FK_ProductCategory_Categories_category_id",
                        column: x => x.category_id,
                        principalTable: "Categories",
                        principalColumn: "category_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCategory_Products_product_id",
                        column: x => x.product_id,
                        principalTable: "Products",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductOptions",
                columns: table => new
                {
                    product_option_id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    product_id = table.Column<int>(nullable: false),
                    option_id = table.Column<int>(nullable: false),
                    is_required = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOptions", x => x.product_option_id);
                    table.ForeignKey(
                        name: "FK_ProductOptions_Options_option_id",
                        column: x => x.option_id,
                        principalTable: "Options",
                        principalColumn: "option_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductOptions_Products_product_id",
                        column: x => x.product_id,
                        principalTable: "Products",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoreCategory",
                columns: table => new
                {
                    store_id = table.Column<int>(nullable: false),
                    category_id = table.Column<int>(nullable: false),
                    commission = table.Column<int>(nullable: false),
                    currency = table.Column<string>(nullable: true),
                    matched_category = table.Column<string>(nullable: true),
                    shipping_fee = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreCategory", x => new { x.store_id, x.category_id });
                    table.ForeignKey(
                        name: "FK_StoreCategory_Categories_category_id",
                        column: x => x.category_id,
                        principalTable: "Categories",
                        principalColumn: "category_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreCategory_Stores_store_id",
                        column: x => x.store_id,
                        principalTable: "Stores",
                        principalColumn: "store_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoreProduct",
                columns: table => new
                {
                    store_id = table.Column<int>(nullable: false),
                    product_id = table.Column<int>(nullable: false),
                    commission = table.Column<int>(nullable: false),
                    currency = table.Column<string>(nullable: true),
                    matched_category = table.Column<string>(nullable: true),
                    shipping_fee = table.Column<decimal>(nullable: false),
                    sale_price = table.Column<decimal>(nullable: false),
                    strikethrough_price = table.Column<decimal>(nullable: false),
                    origin = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    url = table.Column<string>(nullable: true),
                    is_active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreProduct", x => new { x.store_id, x.product_id });
                    table.ForeignKey(
                        name: "FK_StoreProduct_Products_product_id",
                        column: x => x.product_id,
                        principalTable: "Products",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreProduct_Stores_store_id",
                        column: x => x.store_id,
                        principalTable: "Stores",
                        principalColumn: "store_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoreUser",
                columns: table => new
                {
                    store_id = table.Column<int>(nullable: false),
                    user_id = table.Column<int>(nullable: false),
                    api_key = table.Column<string>(nullable: true),
                    secret_key = table.Column<string>(nullable: true),
                    is_confirmed = table.Column<bool>(nullable: false),
                    expire_time = table.Column<DateTime>(nullable: false),
                    is_active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreUser", x => new { x.store_id, x.user_id });
                    table.ForeignKey(
                        name: "FK_StoreUser_Stores_store_id",
                        column: x => x.store_id,
                        principalTable: "Stores",
                        principalColumn: "store_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreUser_SystemUser_user_id",
                        column: x => x.user_id,
                        principalTable: "SystemUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductOptionValues",
                columns: table => new
                {
                    product_option_value_id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    product_option_id = table.Column<int>(nullable: false),
                    option_value_id = table.Column<int>(nullable: false),
                    quantity = table.Column<int>(nullable: false),
                    price = table.Column<decimal>(nullable: false),
                    weight = table.Column<decimal>(nullable: false),
                    point = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOptionValues", x => x.product_option_value_id);
                    table.ForeignKey(
                        name: "FK_ProductOptionValues_OptionValues_option_value_id",
                        column: x => x.option_value_id,
                        principalTable: "OptionValues",
                        principalColumn: "option_value_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductOptionValues_ProductOptions_product_option_id",
                        column: x => x.product_option_id,
                        principalTable: "ProductOptions",
                        principalColumn: "product_option_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OptionValues_option_id",
                table: "OptionValues",
                column: "option_id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_category_id",
                table: "ProductCategory",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOptions_option_id",
                table: "ProductOptions",
                column: "option_id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOptions_product_id",
                table: "ProductOptions",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOptionValues_option_value_id",
                table: "ProductOptionValues",
                column: "option_value_id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOptionValues_product_option_id_option_value_id",
                table: "ProductOptionValues",
                columns: new[] { "product_option_id", "option_value_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StoreCategory_category_id",
                table: "StoreCategory",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_StoreProduct_product_id",
                table: "StoreProduct",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_StoreUser_api_key",
                table: "StoreUser",
                column: "api_key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StoreUser_user_id",
                table: "StoreUser",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductCategory");

            migrationBuilder.DropTable(
                name: "ProductOptionValues");

            migrationBuilder.DropTable(
                name: "StoreCategory");

            migrationBuilder.DropTable(
                name: "StoreProduct");

            migrationBuilder.DropTable(
                name: "StoreUser");

            migrationBuilder.DropTable(
                name: "OptionValues");

            migrationBuilder.DropTable(
                name: "ProductOptions");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Stores");

            migrationBuilder.DropTable(
                name: "SystemUser");

            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
