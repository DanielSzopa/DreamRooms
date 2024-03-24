using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Staff.Core.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_ModuleProperty_To_OutBox : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Module",
                schema: "staff",
                table: "OutboxMessages",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Module",
                schema: "staff",
                table: "OutboxMessages");
        }
    }
}
