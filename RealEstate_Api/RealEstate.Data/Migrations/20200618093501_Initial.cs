using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstate.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoleClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserLogins",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: true),
                    ProviderKey = table.Column<string>(nullable: true),
                    ProviderDisplayName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserLogins", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "AppUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserRoles", x => new { x.UserId, x.RoleId });
                });

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
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
                    FullName = table.Column<string>(maxLength: 200, nullable: false),
                    Address = table.Column<string>(nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    IdentityNumber = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserTokens", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Directions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DirectionName = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EvaluationStatusName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LegalPapers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeOfLegalPapers = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalPapers", x => x.Id);
                });

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
                name: "Properties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 200, nullable: false),
                    ApartmentNumber = table.Column<string>(maxLength: 200, nullable: false),
                    StreetNames = table.Column<string>(maxLength: 200, nullable: false),
                    WardId = table.Column<int>(nullable: false),
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
                    Description = table.Column<string>(nullable: false),
                    EvaluationStatusId = table.Column<int>(nullable: true),
                    LegalPapersId = table.Column<int>(nullable: true),
                    TypeOfPropertyId = table.Column<int>(nullable: false),
                    TypeOfTransactionId = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 18, 16, 34, 58, 296, DateTimeKind.Local).AddTicks(9603)),
                    EndDate = table.Column<DateTime>(nullable: false),
                    NumberOfStoreys = table.Column<int>(nullable: true),
                    NumberOfBedrooms = table.Column<int>(nullable: true),
                    NumberOfWCs = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false, defaultValue: 0),
                    HouseDirectionId = table.Column<int>(nullable: true),
                    Lat = table.Column<double>(nullable: true),
                    Lng = table.Column<double>(nullable: true),
                    ContactName = table.Column<string>(maxLength: 100, nullable: true),
                    EmailContact = table.Column<string>(maxLength: 100, nullable: true),
                    ContactPhone = table.Column<string>(maxLength: 100, nullable: true),
                    UserID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Properties_EvaluationStatuses_EvaluationStatusId",
                        column: x => x.EvaluationStatusId,
                        principalTable: "EvaluationStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Properties_Directions_HouseDirectionId",
                        column: x => x.HouseDirectionId,
                        principalTable: "Directions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Properties_LegalPapers_LegalPapersId",
                        column: x => x.LegalPapersId,
                        principalTable: "LegalPapers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    table.ForeignKey(
                        name: "FK_Properties_AppUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Properties_Wards_WardId",
                        column: x => x.WardId,
                        principalTable: "Wards",
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
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("da131b40-8f65-4040-b257-aa9a2ef7be3e"), "fd1007f3-7dd0-413e-8118-fdccfd96e2cd", "Administrator role", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { new Guid("6b51bd7e-d928-409f-8404-c535bd73d04c"), new Guid("da131b40-8f65-4040-b257-aa9a2ef7be3e") });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "Gender", "IdentityNumber", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("6b51bd7e-d928-409f-8404-c535bd73d04c"), 0, "85/4 Nguyễn Thế Truyện, phường Tân Sơn Nhì, quận Tân Phú", "4c2a777f-4d36-4f8d-8647-1e1abc26c916", "admin@gmail.com", true, "Tôn Võ Thủy Tiên", 1, "0123456789", false, null, "admin@gmail.com", "admin", "AQAAAAEAACcQAAAAEFX11Xs4eoi4iiCfsdx9s281KJErhG3N1jajBpkNBB0MKctTPY+hRM/AWpRyjCEUsA==", null, false, "", false, "admin" });

            migrationBuilder.InsertData(
                table: "Directions",
                columns: new[] { "Id", "DirectionName" },
                values: new object[,]
                {
                    { 1, "Đông" },
                    { 2, "Tây" },
                    { 3, "Nam" },
                    { 4, "Bắc" },
                    { 5, "Đông Bắc" },
                    { 6, "Tây Bắc" },
                    { 7, "Đông Nam" },
                    { 8, "Tây Nam" }
                });

            migrationBuilder.InsertData(
                table: "EvaluationStatuses",
                columns: new[] { "Id", "EvaluationStatusName" },
                values: new object[,]
                {
                    { 2, "Chưa Thẩm Định" },
                    { 1, "Đã Thẩm Định" }
                });

            migrationBuilder.InsertData(
                table: "LegalPapers",
                columns: new[] { "Id", "TypeOfLegalPapers" },
                values: new object[,]
                {
                    { 1, "Sổ Hồng" },
                    { 2, "Sổ Đỏ" },
                    { 3, "Giấy Tay" },
                    { 4, "Giấy Tờ Hợp Lệ" },
                    { 5, "Đang Hợp Thức Hóa" },
                    { 6, "Chủ Quyền Tư Nhân" },
                    { 7, "Hợp Đồng" }
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
                name: "IX_Properties_EvaluationStatusId",
                table: "Properties",
                column: "EvaluationStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_HouseDirectionId",
                table: "Properties",
                column: "HouseDirectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_LegalPapersId",
                table: "Properties",
                column: "LegalPapersId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_TypeOfPropertyId",
                table: "Properties",
                column: "TypeOfPropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_TypeOfTransactionId",
                table: "Properties",
                column: "TypeOfTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_UserID",
                table: "Properties",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_WardId",
                table: "Properties",
                column: "WardId");

            migrationBuilder.CreateIndex(
                name: "IX_Wards_DistrictId",
                table: "Wards",
                column: "DistrictId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppRoleClaims");

            migrationBuilder.DropTable(
                name: "AppRoles");

            migrationBuilder.DropTable(
                name: "AppUserClaims");

            migrationBuilder.DropTable(
                name: "AppUserLogins");

            migrationBuilder.DropTable(
                name: "AppUserRoles");

            migrationBuilder.DropTable(
                name: "AppUserTokens");

            migrationBuilder.DropTable(
                name: "ImageProperties");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "EvaluationStatuses");

            migrationBuilder.DropTable(
                name: "Directions");

            migrationBuilder.DropTable(
                name: "LegalPapers");

            migrationBuilder.DropTable(
                name: "TypeOfPrperties");

            migrationBuilder.DropTable(
                name: "TypeOfTransactions");

            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "Wards");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "Provinces");
        }
    }
}
