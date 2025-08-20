using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookworm.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "beneficiary_master",
                columns: table => new
                {
                    ben_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ben_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ben_email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ben_pan = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_beneficiary_master", x => x.ben_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "customer_master",
                columns: table => new
                {
                    customer_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    address = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    age = table.Column<int>(type: "int", nullable: true),
                    dob = table.Column<DateTime>(type: "date", nullable: true),
                    email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password_hash = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    phone = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer_master", x => x.customer_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "genre_master",
                columns: table => new
                {
                    genre_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    genre_desc = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_genre_master", x => x.genre_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "language_master",
                columns: table => new
                {
                    language_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    language_desc = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_language_master", x => x.language_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "product_type_master",
                columns: table => new
                {
                    type_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    type_desc = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_type_master", x => x.type_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.role_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cart_master",
                columns: table => new
                {
                    cart_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    customer_id = table.Column<int>(type: "int", nullable: false),
                    cost = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cart_master", x => x.cart_id);
                    table.ForeignKey(
                        name: "FK_cart_master_customer_master_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customer_master",
                        principalColumn: "customer_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "product_master",
                columns: table => new
                {
                    product_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    product_author = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    product_base_price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    english_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    img_src = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_rentable = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    product_isbn = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    long_description = table.Column<string>(type: "TEXT", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    min_rent_days = table.Column<int>(type: "int", nullable: true),
                    product_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    product_offer_price = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    rent_per_day = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    product_sp_cost = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    short_description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    genre_id = table.Column<int>(type: "int", nullable: false),
                    language_id = table.Column<int>(type: "int", nullable: false),
                    type_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_master", x => x.product_id);
                    table.ForeignKey(
                        name: "FK_product_master_genre_master_genre_id",
                        column: x => x.genre_id,
                        principalTable: "genre_master",
                        principalColumn: "genre_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_master_language_master_language_id",
                        column: x => x.language_id,
                        principalTable: "language_master",
                        principalColumn: "language_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_master_product_type_master_type_id",
                        column: x => x.type_id,
                        principalTable: "product_type_master",
                        principalColumn: "type_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "customer_roles",
                columns: table => new
                {
                    customer_id = table.Column<int>(type: "int", nullable: false),
                    role_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer_roles", x => new { x.customer_id, x.role_id });
                    table.ForeignKey(
                        name: "FK_customer_roles_customer_master_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customer_master",
                        principalColumn: "customer_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_customer_roles_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "invoice_master",
                columns: table => new
                {
                    invoice_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    cart_id = table.Column<int>(type: "int", nullable: true),
                    customer_id = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_invoice_master", x => x.invoice_id);
                    table.ForeignKey(
                        name: "FK_invoice_master_cart_master_cart_id",
                        column: x => x.cart_id,
                        principalTable: "cart_master",
                        principalColumn: "cart_id");
                    table.ForeignKey(
                        name: "FK_invoice_master_customer_master_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customer_master",
                        principalColumn: "customer_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cart_details",
                columns: table => new
                {
                    cart_details_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    is_rented = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    is_updated = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    offer_cost = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    rent_no_of_days = table.Column<int>(type: "int", nullable: true),
                    cart_id = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cart_details", x => x.cart_details_id);
                    table.ForeignKey(
                        name: "FK_cart_details_cart_master_cart_id",
                        column: x => x.cart_id,
                        principalTable: "cart_master",
                        principalColumn: "cart_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cart_details_product_master_product_id",
                        column: x => x.product_id,
                        principalTable: "product_master",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "product_beneficiary",
                columns: table => new
                {
                    product_beneficiary_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    percentage = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    beneficiary_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_beneficiary", x => x.product_beneficiary_id);
                    table.ForeignKey(
                        name: "FK_product_beneficiary_beneficiary_master_beneficiary_id",
                        column: x => x.beneficiary_id,
                        principalTable: "beneficiary_master",
                        principalColumn: "ben_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_beneficiary_product_master_product_id",
                        column: x => x.product_id,
                        principalTable: "product_master",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "invoice_details",
                columns: table => new
                {
                    inv_dtl_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    rent_no_of_days = table.Column<int>(type: "int", nullable: true),
                    royalty_amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    sell_price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    tran_type = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    invoice_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_invoice_details", x => x.inv_dtl_id);
                    table.ForeignKey(
                        name: "FK_invoice_details_invoice_master_invoice_id",
                        column: x => x.invoice_id,
                        principalTable: "invoice_master",
                        principalColumn: "invoice_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_invoice_details_product_master_product_id",
                        column: x => x.product_id,
                        principalTable: "product_master",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "royalty_calculation",
                columns: table => new
                {
                    royalty_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    royalty_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    transaction_type = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sales_price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    royalty_on_sales_price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    beneficiary_id = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    invoice_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_royalty_calculation", x => x.royalty_id);
                    table.ForeignKey(
                        name: "FK_royalty_calculation_beneficiary_master_beneficiary_id",
                        column: x => x.beneficiary_id,
                        principalTable: "beneficiary_master",
                        principalColumn: "ben_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_royalty_calculation_invoice_master_invoice_id",
                        column: x => x.invoice_id,
                        principalTable: "invoice_master",
                        principalColumn: "invoice_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_royalty_calculation_product_master_product_id",
                        column: x => x.product_id,
                        principalTable: "product_master",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user_library",
                columns: table => new
                {
                    user_library_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    customer_id = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    invoice_detail_id = table.Column<int>(type: "int", nullable: false),
                    acquisition_type = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    acquisition_timestamp = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_library", x => x.user_library_id);
                    table.ForeignKey(
                        name: "FK_user_library_customer_master_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customer_master",
                        principalColumn: "customer_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_library_invoice_details_invoice_detail_id",
                        column: x => x.invoice_detail_id,
                        principalTable: "invoice_details",
                        principalColumn: "inv_dtl_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_library_product_master_product_id",
                        column: x => x.product_id,
                        principalTable: "product_master",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "rental_ledger",
                columns: table => new
                {
                    user_library_id = table.Column<long>(type: "bigint", nullable: false),
                    rent_start_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    rent_end_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    last_status_check = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rental_ledger", x => x.user_library_id);
                    table.ForeignKey(
                        name: "FK_rental_ledger_user_library_user_library_id",
                        column: x => x.user_library_id,
                        principalTable: "user_library",
                        principalColumn: "user_library_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_beneficiary_master_ben_email",
                table: "beneficiary_master",
                column: "ben_email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_beneficiary_master_ben_pan",
                table: "beneficiary_master",
                column: "ben_pan",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cart_details_cart_id",
                table: "cart_details",
                column: "cart_id");

            migrationBuilder.CreateIndex(
                name: "IX_cart_details_product_id",
                table: "cart_details",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_cart_master_customer_id",
                table: "cart_master",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_customer_master_email",
                table: "customer_master",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_customer_master_phone",
                table: "customer_master",
                column: "phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_customer_roles_role_id",
                table: "customer_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_invoice_details_invoice_id",
                table: "invoice_details",
                column: "invoice_id");

            migrationBuilder.CreateIndex(
                name: "IX_invoice_details_product_id",
                table: "invoice_details",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_invoice_master_cart_id",
                table: "invoice_master",
                column: "cart_id");

            migrationBuilder.CreateIndex(
                name: "IX_invoice_master_customer_id",
                table: "invoice_master",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_beneficiary_beneficiary_id",
                table: "product_beneficiary",
                column: "beneficiary_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_beneficiary_product_id",
                table: "product_beneficiary",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_master_genre_id",
                table: "product_master",
                column: "genre_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_master_language_id",
                table: "product_master",
                column: "language_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_master_product_isbn",
                table: "product_master",
                column: "product_isbn",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_product_master_type_id",
                table: "product_master",
                column: "type_id");

            migrationBuilder.CreateIndex(
                name: "IX_roles_name",
                table: "roles",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_royalty_calculation_beneficiary_id",
                table: "royalty_calculation",
                column: "beneficiary_id");

            migrationBuilder.CreateIndex(
                name: "IX_royalty_calculation_invoice_id",
                table: "royalty_calculation",
                column: "invoice_id");

            migrationBuilder.CreateIndex(
                name: "IX_royalty_calculation_product_id",
                table: "royalty_calculation",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_library_customer_id",
                table: "user_library",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_library_invoice_detail_id",
                table: "user_library",
                column: "invoice_detail_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_library_product_id",
                table: "user_library",
                column: "product_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cart_details");

            migrationBuilder.DropTable(
                name: "customer_roles");

            migrationBuilder.DropTable(
                name: "product_beneficiary");

            migrationBuilder.DropTable(
                name: "rental_ledger");

            migrationBuilder.DropTable(
                name: "royalty_calculation");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "user_library");

            migrationBuilder.DropTable(
                name: "beneficiary_master");

            migrationBuilder.DropTable(
                name: "invoice_details");

            migrationBuilder.DropTable(
                name: "invoice_master");

            migrationBuilder.DropTable(
                name: "product_master");

            migrationBuilder.DropTable(
                name: "cart_master");

            migrationBuilder.DropTable(
                name: "genre_master");

            migrationBuilder.DropTable(
                name: "language_master");

            migrationBuilder.DropTable(
                name: "product_type_master");

            migrationBuilder.DropTable(
                name: "customer_master");
        }
    }
}
