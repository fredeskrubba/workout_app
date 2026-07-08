using WorkoutApp.Modules.Users.Domain;

namespace WorkoutApplication.Modules.Users.Features.GetUser
{
    public static class GetUser
    {
        public record Query(Guid UserId);

        public record Response(
            Guid Id,
            string Username,
            string Email
        );

        public static Response Handle(Query query)
        {
            // Mock data
            var user = new User(
                Guid.NewGuid(),
                "Frederik",
                "frederik@example.com"
            );

            return new Response(
                user.Id,
                user.Username,
                user.Email
            );
        }
    }
}
