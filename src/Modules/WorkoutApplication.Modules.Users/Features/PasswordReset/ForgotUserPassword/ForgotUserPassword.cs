using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using WorkoutApplication.Modules.Users.Features.CreateUser;
using WorkoutApplication.Modules.Users.Helpers;
using WorkoutApplication.Shared.Data;
using WorkoutApplication.Shared.Entities;
using WorkoutApplication.Shared.Results;
using WorkoutApplication.Shared.Services.Email;

namespace WorkoutApplication.Modules.Users.Features.PasswordReset.ForgotUserPassword
{
    public class ForgotUserPassword
    {
        private readonly WorkoutApplicationDBContext _context;
        private readonly TokenHelper _tokenHelper;
        private readonly IEmailService _emailService;

        public ForgotUserPassword(WorkoutApplicationDBContext context, TokenHelper tokenHelper, IEmailService emailService)
        {
            _context = context;
            _tokenHelper = tokenHelper;
            _emailService = emailService;
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

            string resetLink = $"{resetToken}";

            var emailBody = $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset='UTF-8'>
                    <title>Password Reset</title>
                </head>
                <body>
                    <h2>Reset your Workout App password</h2>

                    <p>
                        We received a request to reset the password for your Workout App account.
                    </p>

                    <p>
                        If you made this request, use the following token:
                    </p>

                    <p>
                        {resetLink}
                        </a>
                    </p>

                    <p>
                        This link will expire in 30 minutes.
                    </p>

                    <p>
                        If you did not request a password reset, you can safely ignore this email.
                        Your password will remain unchanged.
                    </p>

                    <br>

                    <p>
                        Best regards,<br>
                        Frederik Skrubbeltrang
                    </p>
                </body>
                </html>
                ";

            await _emailService.SendEmailAsync(
                user.Email,
                "Reset your Workout App password",
                emailBody
            );

            return Result<ForgotUserPasswordResponse?>.Success(new ForgotUserPasswordResponse("Reset link sent to email"));
        }
    }
}
