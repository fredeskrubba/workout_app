using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using WorkoutApplication.Modules.Users.Features.CreateUser;
using WorkoutApplication.Modules.Users.Features.UpdateUserPassword;
using WorkoutApplication.Shared.Data;
using WorkoutApplication.Shared.Entities;
using WorkoutApplication.Shared.Migrations;
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
            var hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(request.Token));
            var tokenHash = Convert.ToHexString(hashBytes);

            var existingToken = await _context.ResetTokens.FirstOrDefaultAsync(t => t.TokenHash == tokenHash);

            if(existingToken is null)
            {
                return Result<ResetUserPasswordResponse>.Failure("Invalid token");
            }

            if (existingToken.IsUsed)
            {
                return Result<ResetUserPasswordResponse>.Failure("Token has already been used");
            }

            if (existingToken.ExpiresAt < DateTime.UtcNow)
            {
                return Result<ResetUserPasswordResponse>.Failure("Token has expired");
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == existingToken.UserId);

            if (user is null)
            {
                return Result<ResetUserPasswordResponse>.Failure("User not found");
            }

            var newHashedPassword = new PasswordHasher<User>().HashPassword(user, request.NewPassword);

            user.HashedPassword = newHashedPassword;
            existingToken.IsUsed = true;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return Result<ResetUserPasswordResponse>.Failure("Something went wrong, see error: " + ex.Message);
            }

            return Result<ResetUserPasswordResponse?>.Success(new ResetUserPasswordResponse());
        }
    }
}
