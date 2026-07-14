namespace WorkoutApplication.Modules.Users.Entities;

public class User
{
    public int UserId { get; init; }
    public string FirstName { get; init; }

    public string LastName { get; init; }
    public string Email { get; init; }

    public string HashedPassword { get; set; } = string.Empty;

    public string Role { get; set; } = "user";

    public string RefreshToken { get; set; }

    public DateTime? RefreshTokenExpiryTime { get; set; }
    public User(string firstName, string lastName, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;

    }
}
