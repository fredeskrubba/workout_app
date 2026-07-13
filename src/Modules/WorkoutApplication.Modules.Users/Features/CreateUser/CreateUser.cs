using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WorkoutApplication.Modules.Users.Data;
using WorkoutApplication.Modules.Users.Entities;
using WorkoutApplication.Modules.Users.Features.GetUser;
using WorkoutApplication.Shared.Results;
namespace WorkoutApplication.Modules.Users.Features.CreateUser
{
    public class CreateUser
    {
        private readonly UserDBContext _context;

        public CreateUser(UserDBContext context)
        {
            _context = context;
        }
        public async Task<Result<CreateUserResponse>> Handle(CreateUserRequest request)
        {
            var user = new User(request.FirstName, request.LastName, request.Email);
            if(await _context.Users.AnyAsync(u => u.Email.ToLower() == request.Email.ToLower()))
            {
                return Result<CreateUserResponse>.Failure("Email already in use");
            }

            var hashedPassword = new PasswordHasher<User>()
                .HashPassword(user, request.Password);

            user.HashedPassword = hashedPassword;

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            CreateUserResponse createdUser = new(user.FirstName,
            user.LastName,
            user.Email,
            user.HashedPassword);

            return Result<CreateUserResponse>.Success(createdUser);
        }
    }
}
