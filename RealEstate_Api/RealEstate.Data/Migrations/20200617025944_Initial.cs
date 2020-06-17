using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstate.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Code = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfPrperties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeOfPropertyName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfPrperties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeOfTransactionName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfTransactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Prefix = table.Column<string>(maxLength: 20, nullable: false),
                    ProvinceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Districts_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 200, nullable: false),
                    Address = table.Column<string>(maxLength: 200, nullable: false),
                    Image = table.Column<string>(maxLength: 200, nullable: false),
                    Area = table.Column<double>(nullable: false),
                    AreaFrom = table.Column<double>(nullable: true),
                    AreaTo = table.Column<double>(nullable: true),
                    Length = table.Column<double>(nullable: false),
                    Width = table.Column<double>(nullable: false),
                    Facade = table.Column<double>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    PriceFrom = table.Column<decimal>(nullable: true),
                    PriceTo = table.Column<decimal>(nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: false),
                    EvaluationStatus = table.Column<int>(nullable: true),
                    LegalPapers = table.Column<string>(nullable: true),
                    TypeOfPropertyId = table.Column<int>(nullable: false),
                    TypeOfTransactionId = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 17, 9, 59, 41, 349, DateTimeKind.Local).AddTicks(4869)),
                    EndDate = table.Column<DateTime>(nullable: false),
                    NumberOfStoreys = table.Column<int>(nullable: true),
                    NumberOfBedrooms = table.Column<int>(nullable: true),
                    NumberOfWCs = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false, defaultValue: 0),
                    HouseDirection = table.Column<string>(nullable: true),
                    Lat = table.Column<double>(nullable: true),
                    Lng = table.Column<double>(nullable: true),
                    ContactName = table.Column<string>(nullable: true),
                    EmailContact = table.Column<string>(nullable: true),
                    ContactPhone = table.Column<string>(nullable: true),
                    UserID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Properties_TypeOfPrperties_TypeOfPropertyId",
                        column: x => x.TypeOfPropertyId,
                        principalTable: "TypeOfPrperties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Properties_TypeOfTransactions_TypeOfTransactionId",
                        column: x => x.TypeOfTransactionId,
                        principalTable: "TypeOfTransactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Streets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Prefix = table.Column<string>(maxLength: 20, nullable: false),
                    DistrictId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Streets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Streets_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Prefix = table.Column<string>(maxLength: 20, nullable: false),
                    DistrictId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wards_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImageProperties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyId = table.Column<int>(nullable: false),
                    LinkName = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageProperties", x => new { x.Id, x.PropertyId });
                    table.ForeignKey(
                        name: "FK_ImageProperties_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TypeOfPrperties",
                columns: new[] { "Id", "TypeOfPropertyName" },
                values: new object[,]
                {
                    { 1, "Chung Cư/ Căn Hộ" },
                    { 2, "Nhà Riêng" },
                    { 3, "Đất Nền" }
                });

            migrationBuilder.InsertData(
                table: "TypeOfTransactions",
                columns: new[] { "Id", "TypeOfTransactionName" },
                values: new object[,]
                {
                    { 1, "Nhà Đất Bán" },
                    { 2, "Nhà Đất Cho Thuê" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Districts_ProvinceId",
                table: "Districts",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageProperties_PropertyId",
                table: "ImageProperties",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_TypeOfPropertyId",
                table: "Properties",
                column: "TypeOfPropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_TypeOfTransactionId",
                table: "Properties",
                column: "TypeOfTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Streets_DistrictId",
                table: "Streets",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Wards_DistrictId",
                table: "Wards",
                column: "DistrictId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImageProperties");

            migrationBuilder.DropTable(
                name: "Streets");

            migrationBuilder.DropTable(
                name: "Wards");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "TypeOfPrperties");

            migrationBuilder.DropTable(
                name: "TypeOfTransactions");

            migrationBuilder.DropTable(
                name: "Provinces");
        }
    }
}
