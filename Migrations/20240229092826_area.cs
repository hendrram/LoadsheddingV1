using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoadsheddingV1.Migrations
{
    public partial class area : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AreaId",
                table: "LoadSheddingEvents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "LoadSheddingEvents");
        }
    }
}
