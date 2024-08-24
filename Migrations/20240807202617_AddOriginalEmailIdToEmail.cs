using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gmail.Migrations
{
    /// <inheritdoc />
    public partial class AddOriginalEmailIdToEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsReply",
                table: "Emails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "OriginalEmailId",
                table: "Emails",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReply",
                table: "Emails");

            migrationBuilder.DropColumn(
                name: "OriginalEmailId",
                table: "Emails");
        }
    }
}
