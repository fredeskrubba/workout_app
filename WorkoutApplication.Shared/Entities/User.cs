using System.ComponentModel.DataAnnotations.Schema;

namespace WorkoutApplication.Shared.Entities;

[Table("users")]
public class User
{
    [Column("user_id")]
    public int UserId { get; init; }
    [Column("first_name")]
    public string FirstName { get; init; }

    [Column("last_name")]
    public string LastName { get; init; }
    [Column("email")]
    public string Email { get; init; }
    [Column("hashed_password")]

    public string HashedPassword { get; set; } = string.Empty;

    public string Role { get; set; } = "user";

    public string RefreshToken { get; set; } = string.Empty;

    public DateTime? RefreshTokenExpiryTime { get; set; }
    public User(string firstName, string lastName, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;

    }
}
