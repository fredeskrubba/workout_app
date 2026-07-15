using Microsoft.EntityFrameworkCore;
using WorkoutApplication.Shared.Data;

namespace WorkoutApplication.Modules.Users.Features.GetUser
{
    public class GetUser
    {
        private readonly WorkoutApplicationDBContext _context;

        public GetUser(WorkoutApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<GetUserResponse?> Handle(GetUserRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == request.UserId);

            if (user == null)
            {
                return null;
            }

            return new GetUserResponse(
                user.UserId,
                user.FirstName,
                user.LastName,
                user.Email
            );
        }
    }
}
