using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkoutApplication.Shared.Data;
using WorkoutApplication.Shared.Entities;
using WorkoutApplication.Shared.Results;

namespace WorkoutApplication.Modules.Users.Features.PasswordReset.ResetUserPassword
{
    public class ResetUserPassword
    {
        private readonly WorkoutApplicationDBContext _context;

        public ResetUserPassword(WorkoutApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Result<ResetUserPasswordResponse?>> Handle(ResetUserPasswordRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

            if (user is null)
            {
                return Result<ResetUserPasswordResponse?>.Failure("User not found");
            }

            var newHashedPassword = new PasswordHasher<User>().HashPassword(user, request.NewPassword);
            user.HashedPassword = newHashedPassword;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return Result<ResetUserPasswordResponse?>.Failure("Something went wrong, see error: " + ex.Message);
            }

            return Result<ResetUserPasswordResponse?>.Success(new ResetUserPasswordResponse(newHashedPassword));
        }
    }
}
