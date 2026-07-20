using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutApplication.Shared.Migrations
{
    /// <inheritdoc />
    public partial class exercise_sets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Sets",
                table: "session_exercises",
                type: "integer",
                nullable: false,
                defaultValue: 8);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sets",
                table: "session_exercises");
        }
    }
}
