
namespace WorkoutApplication.Modules.Users.Features.GetUser
{
    public record GetUserResponse(
        int Id,
        string FirstName,
        string LastName,
        string Email
    );
}
