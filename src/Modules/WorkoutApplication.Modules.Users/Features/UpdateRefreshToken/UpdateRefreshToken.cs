using Microsoft.EntityFrameworkCore;
using WorkoutApplication.Shared.Entities;
using WorkoutApplication.Modules.Users.Helpers;
using WorkoutApplication.Shared.Data;
using WorkoutApplication.Shared.Results;

namespace WorkoutApplication.Modules.Users.Features.UpdateRefreshToken
{
    public class UpdateRefreshToken
    {
        private readonly WorkoutApplicationDBContext _context;
        private readonly TokenHelper _tokenHelper;

        public UpdateRefreshToken(WorkoutApplicationDBContext context, TokenHelper tokenHelper)
        {
            _context = context;
            _tokenHelper = tokenHelper;
        }

        public async Task<Result<UpdateRefreshTokenResponse>> Handle(UpdateRefreshTokenRequest request)
        {
            var user = await ValidateRefreshTokenAsync(request.UserId, request.RefreshToken);

            if(user is null )
            {
                return Result<UpdateRefreshTokenResponse>.Failure("User not found");
            }

            var accessToken = _tokenHelper.CreateToken(user);

            var refreshToken = _tokenHelper.GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return Result<UpdateRefreshTokenResponse>.Failure("Something went wrong, see error: " + ex.Message);
            }

            return Result<UpdateRefreshTokenResponse>.Success(new UpdateRefreshTokenResponse(accessToken, refreshToken));
        }


        private async Task<User?> ValidateRefreshTokenAsync(int userId, string refreshToken)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return null;
            }

            return user;
        }

        
    }
}
