namespace WorkoutApplication.Modules.Users.Features.CreateUser
{
    public record CreateUserResponse(string FirstName, string LastName, string Email, string HashedPassword);

}
