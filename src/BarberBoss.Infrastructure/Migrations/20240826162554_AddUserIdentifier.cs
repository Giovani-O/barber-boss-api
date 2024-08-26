using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberBoss.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdentifier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientName",
                table: "Incomes");

            migrationBuilder.AddColumn<Guid>(
                name: "UserIdentifier",
                table: "Users",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserIdentifier",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "ClientName",
                table: "Incomes",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
