using System.ComponentModel.DataAnnotations.Schema;
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
}