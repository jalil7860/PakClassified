using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PakClassified.Migrations
{
    /// <inheritdoc />
    public partial class imageAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "dbo",
                table: "AdvertisementSubCategories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                schema: "dbo",
                table: "AdvertisementSubCategories",
                type: "nvarchar(max)",
                maxLength: 10000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                schema: "dbo",
                table: "AdvertisementCategories",
                type: "nvarchar(max)",
                maxLength: 10000,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                schema: "dbo",
                table: "AdvertisementSubCategories");

            migrationBuilder.DropColumn(
                name: "Image",
                schema: "dbo",
                table: "AdvertisementSubCategories");

            migrationBuilder.DropColumn(
                name: "Image",
                schema: "dbo",
                table: "AdvertisementCategories");
        }
    }
}
