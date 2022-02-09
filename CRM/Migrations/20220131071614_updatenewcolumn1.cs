using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Migrations
{
    public partial class updatenewcolumn1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Longitude",
                table: "AspNetUsers",
                newName: "AgreementFilePath");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AgreementFilePath",
                table: "AspNetUsers",
                newName: "Longitude");

            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
