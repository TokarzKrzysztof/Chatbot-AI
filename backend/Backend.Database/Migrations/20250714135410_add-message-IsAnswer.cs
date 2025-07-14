using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Database.Migrations
{
    /// <inheritdoc />
    public partial class addmessageIsAnswer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAnswer",
                table: "Messages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAnswer",
                table: "Messages");
        }
    }
}
