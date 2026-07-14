using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WorkoutApplication.Modules.Users.Data;
using WorkoutApplication.Modules.Users.Entities;
using WorkoutApplication.Modules.Users.Features.GetUser;
using WorkoutApplication.Modules.Users.Features.LoginUser;
using WorkoutApplication.Shared.Results;
using WorkoutApplication.Modules.Users.Helpers;

namespace WorkoutApplication.Modules.Users.Features.UpdateUserPassword
{
    public class UpdateUserPassword
    {
        private readonly UserDBContext _context;
        private readonly CreateToken _tokenHelper;

        public UpdateUserPassword(UserDBContext context, CreateToken tokenHelper)
        {
            _context = context;
            _tokenHelper = tokenHelper;
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
