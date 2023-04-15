using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace datatest4.Migrations
{
    public partial class addedeImeiToAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Imei",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imei",
                table: "Users");
        }
    }
}
