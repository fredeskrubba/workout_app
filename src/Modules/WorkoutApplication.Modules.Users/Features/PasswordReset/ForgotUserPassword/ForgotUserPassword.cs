using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using WorkoutApplication.Modules.Users.Features.CreateUser;
using WorkoutApplication.Modules.Users.Helpers;
using WorkoutApplication.Shared.Data;
using WorkoutApplication.Shared.Entities;
using WorkoutApplication.Shared.Results;

namespace WorkoutApplication.Modules.Users.Features.PasswordReset.ForgotUserPassword
{
    public class ForgotUserPassword
    {
        private readonly WorkoutApplicationDBContext _context;
        private readonly TokenHelper _tokenHelper;

        public ForgotUserPassword(WorkoutApplicationDBContext context, TokenHelper tokenHelper)
        {
            _context = context;
            _tokenHelper = tokenHelper;
        }

        public async Task<Result<ForgotUserPasswordResponse?>> Handle(ForgotUserPasswordRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

            if (user is null)
            {
                return Result<ForgotUserPasswordResponse?>.Failure("User not found");
            }

            

            var previousResetToken = await _context.ResetTokens.FirstOrDefaultAsync(t => t.UserId == user.UserId);

            var resetToken = _tokenHelper.GenerateResetToken();

            var hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(resetToken));
            var tokenHash = Convert.ToHexString(hashBytes);

            if(previousResetToken is null)
            {

                PasswordResetToken tokenToSave = new()
                {
                    UserId = user.UserId,
                    TokenHash = tokenHash,
                    ExpiresAt = DateTime.UtcNow.AddMinutes(30),
                    CreatedAt = DateTime.UtcNow
                };

                _context.ResetTokens.Add(tokenToSave);



            } else
            {
                previousResetToken.TokenHash = tokenHash;
                previousResetToken.CreatedAt = DateTime.UtcNow;
                previousResetToken.ExpiresAt = DateTime.UtcNow.AddMinutes(30);
                previousResetToken.IsUsed = false;

            }


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return Result<ForgotUserPasswordResponse>.Failure("Something went wrong, see error: " + ex.Message);
            }

            // send email to user

            return Result<ForgotUserPasswordResponse?>.Success(new ForgotUserPasswordResponse("Reset link sent to email"));
        }
    }
}
