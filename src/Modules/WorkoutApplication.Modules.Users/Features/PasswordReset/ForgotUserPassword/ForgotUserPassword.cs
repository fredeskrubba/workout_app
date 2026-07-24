using Microsoft.EntityFrameworkCore;
using WorkoutApplication.Shared.Data;
using WorkoutApplication.Shared.Entities;
using WorkoutApplication.Shared.Results;

namespace WorkoutApplication.Modules.Users.Features.PasswordReset.ForgotUserPassword
{
    public class ForgotUserPassword
    {
        private readonly WorkoutApplicationDBContext _context;

        public ForgotUserPassword(WorkoutApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Result<ForgotUserPasswordResponse?>> Handle(ForgotUserPasswordRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

            if (user is null)
            {
                return Result<ForgotUserPasswordResponse?>.Failure("User not found");
            }

            // In a real implementation you'd create a password-reset token and send an email.
            // For now return success with a simple message.
            return Result<ForgotUserPasswordResponse?>.Success(new ForgotUserPasswordResponse("Password reset requested"));
        }
    }
}
