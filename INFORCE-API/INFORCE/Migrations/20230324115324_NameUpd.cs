using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace INFORCE.Migrations
{
    /// <inheritdoc />
    public partial class NameUpd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UrlName",
                table: "Urls",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlName",
                table: "Urls");
        }
    }
}
