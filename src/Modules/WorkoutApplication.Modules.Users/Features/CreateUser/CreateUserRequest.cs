using WorkoutApplication.Modules.Users.Entities;

namespace WorkoutApplication.Modules.Users.Features.CreateUser
{
    public record CreateUserRequest(string FirstName, string LastName, string Email, string Password);
}
