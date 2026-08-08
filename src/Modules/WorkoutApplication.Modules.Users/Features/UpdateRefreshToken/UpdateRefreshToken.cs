using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WorkoutApplication.Modules.Users.Helpers;
using WorkoutApplication.Shared.Data;
using WorkoutApplication.Shared.Entities;
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

        public async Task<Result<UpdateRefreshTokenResponse>> Handle(UpdateRefreshTokenRequest request, HttpContext httpContext)
        {
            var refreshToken = httpContext.Request.Cookies["refreshToken"];
            var user = await ValidateRefreshTokenAsync(refreshToken);

            if(user is null )
            {
                return Result<UpdateRefreshTokenResponse>.Failure("User not found");
            }

            var accessToken = _tokenHelper.CreateToken(user);

            var newRefreshToken = _tokenHelper.GenerateRefreshToken();
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

            httpContext.Response.Cookies.Append(
                "refreshToken",
                newRefreshToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddDays(7)
                }
            );

            return Result<UpdateRefreshTokenResponse>.Success(new UpdateRefreshTokenResponse(accessToken));
        }


        private async Task<User?> ValidateRefreshTokenAsync(string refreshToken)
        {
            var user = await _context.Users
        .SingleOrDefaultAsync(u => u.RefreshToken == refreshToken);

            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return null;
            }

            return user;
        }

        
    }
}
