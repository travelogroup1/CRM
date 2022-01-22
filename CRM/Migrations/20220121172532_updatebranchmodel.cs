using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Migrations
{
    public partial class updatebranchmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Added_By",
                table: "branch",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Added_Date",
                table: "branch",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Modified_By",
                table: "branch",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Modified_Date",
                table: "branch",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Added_By",
                table: "branch");

            migrationBuilder.DropColumn(
                name: "Added_Date",
                table: "branch");

            migrationBuilder.DropColumn(
                name: "Modified_By",
                table: "branch");

            migrationBuilder.DropColumn(
                name: "Modified_Date",
                table: "branch");
        }
    }
}
