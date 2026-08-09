using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WorkoutApplication.Modules.Users.Features.DeleteUser;
using WorkoutApplication.Shared.Data;
using WorkoutApplication.Shared.Results;

namespace WorkoutApplication.Modules.Users.Features.LogoutUser
{
    public class LogoutUser
    {
        private readonly WorkoutApplicationDBContext _context;

        public LogoutUser(WorkoutApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Result<LogoutUserResponse>> Handle(LogoutUserRequest request, HttpContext httpContext)
        {

            var user = await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == request.refreshToken);


            if (user is null)
            {
                return Result<LogoutUserResponse>.Failure("User not found");
            }

            httpContext.Response.Cookies.Delete(
                "refreshToken",
                new CookieOptions
                {
                    Path = "/"
                }
            );

            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = null;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return Result<LogoutUserResponse>.Failure("Something went wrong, see error: " + ex.Message);
            }

            return Result<LogoutUserResponse>.Success(new LogoutUserResponse());
        }
    }
}
