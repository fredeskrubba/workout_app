using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using WorkoutApplication.Modules.Users.Data;
using WorkoutApplication.Modules.Users.Entities;
using WorkoutApplication.Modules.Users.Features.LoginUser;
using WorkoutApplication.Modules.Users.Helpers;
using WorkoutApplication.Shared.Results;

namespace WorkoutApplication.Modules.Users.Features.UpdateRefreshToken
{
    public class UpdateRefreshToken
    {
        private readonly IConfiguration _configuration;
        private readonly UserDBContext _context;
        private readonly TokenHelper _tokenHelper;

        public UpdateRefreshToken(IConfiguration configuration, UserDBContext context, TokenHelper tokenHelper)
        {
            _configuration = configuration;
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

            await _context.SaveChangesAsync();

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
