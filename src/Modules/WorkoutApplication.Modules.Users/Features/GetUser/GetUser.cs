using Microsoft.EntityFrameworkCore;
using WorkoutApplication.Modules.Users.Features.DeleteUser;
using WorkoutApplication.Shared.Data;
using WorkoutApplication.Shared.Results;

namespace WorkoutApplication.Modules.Users.Features.GetUser
{
    public class GetUser
    {
        private readonly WorkoutApplicationDBContext _context;

        public GetUser(WorkoutApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Result<GetUserResponse>> Handle(GetUserRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == request.UserId);

            if (user == null)
            {
                return Result<GetUserResponse>.Failure("User not found");
            }


            return Result<GetUserResponse>.Success(new GetUserResponse(
                user.UserId,
                user.FirstName,
                user.LastName,
                user.Email
            ));

            
        }
    }
}
