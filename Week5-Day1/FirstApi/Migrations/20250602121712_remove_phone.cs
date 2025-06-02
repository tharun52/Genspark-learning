using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstApi.Migrations
{
    /// <inheritdoc />
    public partial class remove_phone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Patients");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Patients",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
