using System.ComponentModel.DataAnnotations.Schema;
using WorkoutApplication.Shared.Enums;

namespace WorkoutApplication.Shared.Entities;

[Table("exercises")]
public class Exercise
{
    [Column("id")]
    public int Id { get; set; }
    [Column("name")]
    public string Name { get; set; }
    [Column("description")]
    public string Description { get; set; }
    [Column("exercise_type")]
    public ExerciseType ExerciseType { get; set; }
}