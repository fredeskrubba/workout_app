using WorkoutApp.Modules.Users.Domain;

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
                "frederik@example.com",
                "1212ffdswe112331221"
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
