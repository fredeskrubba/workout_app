namespace WorkoutApp.Modules.Users.Domain;

public class User
{
    public int Id { get; init; }
    public string FirstName { get; init; }

    public string LastName { get; init; }
    public string Email { get; init; }

    public string HashedPassword { get; init; }

    public User(int id, string firstName, string lastName, string email, string hashedPassword)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        HashedPassword = hashedPassword;
    }
}
