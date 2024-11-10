using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CircleApp.Migrations
{
    /// <inheritdoc />
    public partial class Added_Post_IsPrivateFlag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPrivate",
                table: "Posts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPrivate",
                table: "Posts");
        }
    }
}
