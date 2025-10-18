using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PakClassified.Migrations
{
    /// <inheritdoc />
    public partial class featuresUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Features",
                schema: "dbo",
                table: "Advertisements",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                schema: "dbo",
                table: "AdvertisementCategories",
                type: "nvarchar(max)",
                maxLength: 10000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 10000);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Features",
                schema: "dbo",
                table: "Advertisements");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                schema: "dbo",
                table: "AdvertisementCategories",
                type: "nvarchar(max)",
                maxLength: 10000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 10000,
                oldNullable: true);
        }
    }
}
