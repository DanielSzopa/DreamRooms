using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Staff.Core.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddNewPropertiesToOutBox : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ProcessedAt",
                schema: "staff",
                table: "OutboxMessages",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "staff",
                table: "OutboxMessages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                schema: "staff",
                table: "OutboxMessages",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                schema: "staff",
                table: "OutboxMessages");

            migrationBuilder.DropColumn(
                name: "Type",
                schema: "staff",
                table: "OutboxMessages");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ProcessedAt",
                schema: "staff",
                table: "OutboxMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
