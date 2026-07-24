using System.ComponentModel.DataAnnotations.Schema;
namespace WorkoutApplication.Shared.Entities;

[Table("session_exercises")]
public class SessionExercise
{
    [Column("session_exercise_id")]
    public int SessionExerciseId { get; set; }
    [Column("reps")]
    public int Reps { get; set; }
    [Column("session_id")]

    public int SessionId { get; set; }

    [Column("exercise_id")]
    public int ExerciseId { get; set; }
    
    [Column("Weight")]
    public double Weight { get; set; }
    
    [Column("Sets")]
    public int Sets { get; set; }

    public Exercise Exercise { get; set; } = null!;

}