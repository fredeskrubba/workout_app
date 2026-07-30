using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutApplication.Shared.Migrations
{
    /// <inheritdoc />
    public partial class routine_exercise_update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            
            migrationBuilder.RenameColumn(
                name: "Weight",
                table: "routine_exercises",
                newName: "weight");

            migrationBuilder.RenameColumn(
                name: "Sets",
                table: "routine_exercises",
                newName: "sets");


            migrationBuilder.AddColumn<int>(
                name: "seat_setting",
                table: "routine_exercises",
                type: "integer",
                nullable: true);

            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_routine_exercises_exercises_exercise_id",
                table: "routine_exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_routine_exercises_routines_routine_id",
                table: "routine_exercises");

            migrationBuilder.DropPrimaryKey(
                name: "PK_routine_exercises",
                table: "routine_exercises");

            migrationBuilder.DropColumn(
                name: "seat_setting",
                table: "routine_exercises");

            migrationBuilder.RenameTable(
                name: "routine_exercises",
                newName: "RoutineExercises");

            migrationBuilder.RenameColumn(
                name: "weight",
                table: "RoutineExercises",
                newName: "Weight");

            migrationBuilder.RenameColumn(
                name: "sets",
                table: "RoutineExercises",
                newName: "Sets");

            migrationBuilder.RenameIndex(
                name: "IX_routine_exercises_routine_id",
                table: "RoutineExercises",
                newName: "IX_RoutineExercises_routine_id");

            migrationBuilder.RenameIndex(
                name: "IX_routine_exercises_exercise_id",
                table: "RoutineExercises",
                newName: "IX_RoutineExercises_exercise_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoutineExercises",
                table: "RoutineExercises",
                column: "routine_exercise_id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoutineExercises_exercises_exercise_id",
                table: "RoutineExercises",
                column: "exercise_id",
                principalTable: "exercises",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoutineExercises_routines_routine_id",
                table: "RoutineExercises",
                column: "routine_id",
                principalTable: "routines",
                principalColumn: "routine_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
