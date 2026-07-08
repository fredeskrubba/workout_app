using WorkoutApplication.Modules.Users.Entities;

namespace WorkoutApplication.Modules.Users.Features.GetUser
{
    public static class GetUser
    {


        public static GetUserResponse Handle(GetUserRequest query)
        {
            // Mock data
            var user = new User(
                2,
                "Frederik",
                "Gonzales",
                "frederik@example.com"
            );

            return new GetUserResponse(
                user.Id,
                user.FirstName,
                user.LastName,
                user.Email
            );
        }
    }
}
