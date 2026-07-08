namespace WorkoutApplication.Modules.Users.Entities;

public class User
{
    public int Id { get; init; }
    public string FirstName { get; init; }

    public string LastName { get; init; }
    public string Email { get; init; }

    public string HashedPassword { get; set; } = string.Empty;

    public User(int id, string firstName, string lastName, string email)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;

    }
}
