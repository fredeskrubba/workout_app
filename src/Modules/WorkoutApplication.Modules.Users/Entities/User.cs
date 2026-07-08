namespace WorkoutApp.Modules.Users.Domain;

public class User
{
    public Guid Id { get; init; }
    public string Username { get; init; }
    public string Email { get; init; }

    public User(Guid id, string username, string email)
    {
        Id = id;
        Username = username;
        Email = email;
    }
}
