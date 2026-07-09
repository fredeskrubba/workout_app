using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using WorkoutApplication.Modules.Users.Data;
using WorkoutApplication.Modules.Users.Entities;
using WorkoutApplication.Modules.Users.Features.GetUser;

namespace WorkoutApplication.Modules.Users.Features.CreateUser
{
    public class CreateUser
    {
        private readonly UserDBContext _context;

        public CreateUser(UserDBContext context)
        {
            _context = context;
        }
        public async Task<CreateUserResponse> Handle(CreateUserRequest request)
        {
            var user = new User(request.FirstName, request.LastName, request.Email);
           

            var hashedPassword = new PasswordHasher<User>()
                .HashPassword(user, request.Password);

            user.HashedPassword = hashedPassword;

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return new CreateUserResponse(
                user.FirstName,
                user.LastName,
                user.Email,
                user.HashedPassword
            );
        }
    }
}
