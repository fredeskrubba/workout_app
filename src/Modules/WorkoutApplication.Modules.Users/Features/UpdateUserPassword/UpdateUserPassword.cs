using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkoutApplication.Shared.Entities;
using WorkoutApplication.Shared.Data;
using WorkoutApplication.Shared.Results;


namespace WorkoutApplication.Modules.Users.Features.UpdateUserPassword
{
    public class UpdateUserPassword
    {
        private readonly WorkoutApplicationDBContext _context;
        

        public UpdateUserPassword(WorkoutApplicationDBContext context)
        {
            _context = context;
           
        }

        public async Task<Result<UpdateUserPasswordResponse?>> Handle(UpdateUserPasswordRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

            if (user is null)
            {
                return Result<UpdateUserPasswordResponse?>.Failure("User not found");
            }

            var newHashedPassword = new PasswordHasher<User>().HashPassword(user, request.NewPassword);

            user.HashedPassword = newHashedPassword;
            await _context.SaveChangesAsync();

            return Result<UpdateUserPasswordResponse?>.Success(new UpdateUserPasswordResponse(newHashedPassword));

        }
    }
}
