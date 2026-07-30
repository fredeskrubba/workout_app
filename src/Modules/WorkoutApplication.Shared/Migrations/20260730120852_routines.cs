using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WorkoutApplication.Shared.Migrations
{
    /// <inheritdoc />
    public partial class routines : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.CreateTable(
                name: "routines",
                columns: table => new
                {
                    routine_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_routines", x => x.routine_id);
                    table.ForeignKey(
                        name: "FK_routines_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "routine_exercises",
                columns: table => new
                {
                    routine_exercise_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    reps = table.Column<int>(type: "integer", nullable: false),
                    routine_id = table.Column<int>(type: "integer", nullable: false),
                    exercise_id = table.Column<int>(type: "integer", nullable: false),
                    Weight = table.Column<double>(type: "double precision", nullable: false),
                    Sets = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_routine_exercises", x => x.routine_exercise_id);
                    table.ForeignKey(
                        name: "FK_routine_exercises_exercises_exercise_id",
                        column: x => x.exercise_id,
                        principalTable: "exercises",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_routine_exercises_routines_routine_id",
                        column: x => x.routine_id,
                        principalTable: "routines",
                        principalColumn: "routine_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_routine_exercises_exercise_id",
                table: "routine_exercises",
                column: "exercise_id");

            migrationBuilder.CreateIndex(
                name: "IX_routine_exercises_routine_id",
                table: "routine_exercises",
                column: "routine_id");

            migrationBuilder.CreateIndex(
                name: "IX_routines_UserId",
                table: "routines",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "routine_exercises");

            migrationBuilder.DropTable(
                name: "routines");

            migrationBuilder.RenameColumn(
                name: "is_used",
                table: "password_reset_tokens",
                newName: "isUsed");
        }
    }
}
