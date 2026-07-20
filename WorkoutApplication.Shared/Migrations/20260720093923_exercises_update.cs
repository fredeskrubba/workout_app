using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutApplication.Shared.Migrations
{
    /// <inheritdoc />
    public partial class exercises_update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "exercise_type",
                table: "exercises",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "is_primary",
                table: "exercise_muscle_groups",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "exercise_type",
                table: "exercises");

            migrationBuilder.DropColumn(
                name: "is_primary",
                table: "exercise_muscle_groups");
        }
    }
}
