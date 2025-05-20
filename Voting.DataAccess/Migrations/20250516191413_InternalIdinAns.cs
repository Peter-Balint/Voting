using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Voting.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InternalIdinAns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InternalAnswerId",
                table: "Answers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InternalAnswerId",
                table: "Answers");
        }
    }
}
