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


namespace WorkoutApplication.Modules.Users.Features.UpdateUserPassword
{
    public class UpdateUserPassword
    {
        private readonly UserDBContext _context;
        

        public UpdateUserPassword(UserDBContext context)
        {
            _context = context;
           
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
