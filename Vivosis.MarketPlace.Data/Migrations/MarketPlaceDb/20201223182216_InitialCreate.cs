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
                    name = table.Column<string>(nullable: true),
                    path_name = table.Column<string>(nullable: true)
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
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
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
                    ExpireTime = table.Column<DateTime>(nullable: true),
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
                name: "productcategory",
                columns: table => new
                {
                    product_id = table.Column<int>(nullable: false),
                    category_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productcategory", x => new { x.product_id, x.category_id });
                    table.ForeignKey(
                        name: "FK_productcategory_Categories_category_id",
                        column: x => x.category_id,
                        principalTable: "Categories",
                        principalColumn: "category_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_productcategory_Products_product_id",
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
                    is_required = table.Column<bool>(nullable: false),
                    value = table.Column<string>(nullable: true)
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
                name: "CategoryFromStore",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ParentId = table.Column<long>(nullable: false),
                    StoreId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryFromStore", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryFromStore_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "store_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "storecategory",
                columns: table => new
                {
                    store_category_id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    commission = table.Column<int>(nullable: false),
                    currency = table.Column<string>(nullable: true),
                    shipping_fee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    store_id = table.Column<int>(nullable: false),
                    category_id = table.Column<int>(nullable: false),
                    is_matched = table.Column<bool>(nullable: false),
                    matched_category_name = table.Column<string>(nullable: true),
                    matched_category_code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_storecategory", x => x.store_category_id);
                    table.ForeignKey(
                        name: "FK_storecategory_Categories_category_id",
                        column: x => x.category_id,
                        principalTable: "Categories",
                        principalColumn: "category_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_storecategory_Stores_store_id",
                        column: x => x.store_id,
                        principalTable: "Stores",
                        principalColumn: "store_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "storeproduct",
                columns: table => new
                {
                    store_id = table.Column<int>(nullable: false),
                    product_id = table.Column<int>(nullable: false),
                    commission = table.Column<int>(nullable: false),
                    currency = table.Column<string>(nullable: true),
                    shipping_fee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    sale_price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    strikethrough_price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    origin = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    url = table.Column<string>(nullable: true),
                    is_active = table.Column<bool>(nullable: false),
                    matched_product_code = table.Column<string>(nullable: true),
                    is_sent = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_storeproduct", x => new { x.store_id, x.product_id });
                    table.ForeignKey(
                        name: "FK_storeproduct_Products_product_id",
                        column: x => x.product_id,
                        principalTable: "Products",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_storeproduct_Stores_store_id",
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
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    point = table.Column<int>(nullable: false),
                    subtract = table.Column<bool>(nullable: false),
                    IsChanged = table.Column<bool>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "CategoryOptions",
                columns: table => new
                {
                    category_option_id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    store_category_id = table.Column<int>(nullable: false),
                    option_id = table.Column<int>(nullable: false),
                    is_required = table.Column<bool>(nullable: false),
                    matched_store_option_name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryOptions", x => x.category_option_id);
                    table.ForeignKey(
                        name: "FK_CategoryOptions_Options_option_id",
                        column: x => x.option_id,
                        principalTable: "Options",
                        principalColumn: "option_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryOptions_storecategory_store_category_id",
                        column: x => x.store_category_id,
                        principalTable: "storecategory",
                        principalColumn: "store_category_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryOptionValues",
                columns: table => new
                {
                    category_option_value_id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    category_option_id = table.Column<int>(nullable: false),
                    option_value_id = table.Column<int>(nullable: false),
                    store_category_value_id = table.Column<int>(nullable: false),
                    store_category_value_name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryOptionValues", x => x.category_option_value_id);
                    table.ForeignKey(
                        name: "FK_CategoryOptionValues_CategoryOptions_category_option_id",
                        column: x => x.category_option_id,
                        principalTable: "CategoryOptions",
                        principalColumn: "category_option_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryOptionValues_OptionValues_option_value_id",
                        column: x => x.option_value_id,
                        principalTable: "OptionValues",
                        principalColumn: "option_value_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryFromStore_StoreId",
                table: "CategoryFromStore",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryOptions_option_id",
                table: "CategoryOptions",
                column: "option_id");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryOptions_store_category_id",
                table: "CategoryOptions",
                column: "store_category_id");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryOptionValues_option_value_id",
                table: "CategoryOptionValues",
                column: "option_value_id");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryOptionValues_category_option_id_option_value_id",
                table: "CategoryOptionValues",
                columns: new[] { "category_option_id", "option_value_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OptionValues_option_id",
                table: "OptionValues",
                column: "option_id");

            migrationBuilder.CreateIndex(
                name: "IX_productcategory_category_id",
                table: "productcategory",
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
                name: "IX_storecategory_category_id",
                table: "storecategory",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_storecategory_store_id_category_id",
                table: "storecategory",
                columns: new[] { "store_id", "category_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_storeproduct_product_id",
                table: "storeproduct",
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
                name: "CategoryFromStore");

            migrationBuilder.DropTable(
                name: "CategoryOptionValues");

            migrationBuilder.DropTable(
                name: "productcategory");

            migrationBuilder.DropTable(
                name: "ProductOptionValues");

            migrationBuilder.DropTable(
                name: "storeproduct");

            migrationBuilder.DropTable(
                name: "StoreUser");

            migrationBuilder.DropTable(
                name: "CategoryOptions");

            migrationBuilder.DropTable(
                name: "OptionValues");

            migrationBuilder.DropTable(
                name: "ProductOptions");

            migrationBuilder.DropTable(
                name: "SystemUser");

            migrationBuilder.DropTable(
                name: "storecategory");

            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Stores");
        }
    }
}
