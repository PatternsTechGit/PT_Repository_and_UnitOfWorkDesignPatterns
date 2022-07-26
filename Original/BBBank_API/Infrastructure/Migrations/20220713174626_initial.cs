using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfilePicUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AccountStatus = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TransactionType = table.Column<int>(type: "int", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransactionAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "ProfilePicUrl" },
                values: new object[] { "aa45e3c9-261d-41fe-a1b0-5b4dcf79cfd3", "rassmasood@hotmail.com", "Raas", "Masood", "https://res.cloudinary.com/demo/image/upload/w_400,h_400,c_crop,g_face,r_max/w_200/lady.jpg" });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountNumber", "AccountStatus", "AccountTitle", "CurrentBalance", "UserId" },
                values: new object[] { "37846734-172e-4149-8cec-6f43d1eb3f60", "0001-1001", 1, "Raas Masood", 3500m, "aa45e3c9-261d-41fe-a1b0-5b4dcf79cfd3" });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "AccountId", "TransactionAmount", "TransactionDate", "TransactionType" },
                values: new object[,]
                {
                    { "0fc256f8-193e-4a9d-a9de-a99b2fe8985c", "37846734-172e-4149-8cec-6f43d1eb3f60", -200m, new DateTime(2022, 3, 13, 22, 46, 25, 906, DateTimeKind.Local).AddTicks(8211), 1 },
                    { "1bda4dae-ca2d-44f2-8a82-a35b4bbd51da", "37846734-172e-4149-8cec-6f43d1eb3f60", 1000m, new DateTime(2020, 7, 13, 22, 46, 25, 906, DateTimeKind.Local).AddTicks(8192), 0 },
                    { "30a44bf9-2b64-47d3-941c-f5fb0673255b", "37846734-172e-4149-8cec-6f43d1eb3f60", 200m, new DateTime(2022, 1, 13, 22, 46, 25, 906, DateTimeKind.Local).AddTicks(8219), 0 },
                    { "3de866d0-a520-4a3c-a8b8-cae109b956be", "37846734-172e-4149-8cec-6f43d1eb3f60", 200m, new DateTime(2021, 10, 13, 22, 46, 25, 906, DateTimeKind.Local).AddTicks(8230), 0 },
                    { "54a25060-8dfa-48be-adad-e18fbae51b52", "37846734-172e-4149-8cec-6f43d1eb3f60", 500m, new DateTime(2022, 4, 13, 22, 46, 25, 906, DateTimeKind.Local).AddTicks(8206), 0 },
                    { "6a56bd7c-7104-4992-98e3-8bc66cc405d3", "37846734-172e-4149-8cec-6f43d1eb3f60", -500m, new DateTime(2021, 7, 13, 22, 46, 25, 906, DateTimeKind.Local).AddTicks(8187), 1 },
                    { "89e82f7c-c947-473a-a938-0e166ece430a", "37846734-172e-4149-8cec-6f43d1eb3f60", -100m, new DateTime(2021, 11, 13, 22, 46, 25, 906, DateTimeKind.Local).AddTicks(8226), 1 },
                    { "b4596834-cedd-43c6-82fa-c90906d4779c", "37846734-172e-4149-8cec-6f43d1eb3f60", -300m, new DateTime(2021, 12, 13, 22, 46, 25, 906, DateTimeKind.Local).AddTicks(8222), 1 },
                    { "c94bc558-4c28-43da-8174-05cfbeee028a", "37846734-172e-4149-8cec-6f43d1eb3f60", 900m, new DateTime(2021, 8, 13, 22, 46, 25, 906, DateTimeKind.Local).AddTicks(8241), 0 },
                    { "d0ccf4c1-94da-4652-b537-2adb46886ed3", "37846734-172e-4149-8cec-6f43d1eb3f60", -500m, new DateTime(2021, 9, 13, 22, 46, 25, 906, DateTimeKind.Local).AddTicks(8234), 1 },
                    { "d9a5c15d-89ec-4f30-967a-964af3485651", "37846734-172e-4149-8cec-6f43d1eb3f60", 3000m, new DateTime(2022, 7, 12, 22, 46, 25, 906, DateTimeKind.Local).AddTicks(8160), 0 },
                    { "ec2295e6-2d8c-4d75-8cd4-87c5a4cb464a", "37846734-172e-4149-8cec-6f43d1eb3f60", 500m, new DateTime(2022, 2, 13, 22, 46, 25, 906, DateTimeKind.Local).AddTicks(8215), 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserId",
                table: "Accounts",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountId",
                table: "Transactions",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
