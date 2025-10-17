using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CalculatorAPI.Migrations
{
    /// <inheritdoc />
    public partial class migrationdescriptionnameadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Num1",
                table: "Variants");

            migrationBuilder.RenameColumn(
                name: "Num2",
                table: "Variants",
                newName: "Numb");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Variants",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NamsName",
                table: "Variants",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Variants");

            migrationBuilder.DropColumn(
                name: "NamsName",
                table: "Variants");

            migrationBuilder.RenameColumn(
                name: "Numb",
                table: "Variants",
                newName: "Num2");

            migrationBuilder.AddColumn<float>(
                name: "Num1",
                table: "Variants",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
