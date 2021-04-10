using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PantheonTest.Persistence.Migrations
{
    public partial class Transaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AccountId = table.Column<Guid>(nullable: false),
                    TransactionType = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(type: "decimal", nullable: false),
                    Reference = table.Column<string>(maxLength: 125, nullable: false),
                    DateOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");
        }
    }
}
