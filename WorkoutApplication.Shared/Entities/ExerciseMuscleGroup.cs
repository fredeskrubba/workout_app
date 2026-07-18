using System.ComponentModel.DataAnnotations.Schema;
namespace WorkoutApplication.Shared.Entities;

[Table("exercise_muscle_groups")]
public class ExerciseMuscleGroup
{
    
    [Column("exercise_id")]
    public int ExerciseId;
    [Column("muscle_group_id")]
    public int MuscleGroupId;
}