using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ST10390916_PROG7311_POE.Migrations
{
    /// <inheritdoc />
    public partial class ChangedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Products",
                newName: "Category");

            migrationBuilder.RenameColumn(
                name: "DateListed",
                table: "Products",
                newName: "ProductionDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductionDate",
                table: "Products",
                newName: "DateListed");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Products",
                newName: "Description");
        }
    }
}
