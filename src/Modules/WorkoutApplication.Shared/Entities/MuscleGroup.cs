using System.ComponentModel.DataAnnotations.Schema;
namespace WorkoutApplication.Shared.Entities;

[Table("muscle_groups")]
public class MuscleGroup
{
    [Column("id")]
    public int Id { get; set; }
    [Column("name")]
    public string Name { get; set; }
}