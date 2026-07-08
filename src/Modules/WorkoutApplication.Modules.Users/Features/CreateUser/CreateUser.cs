using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using WorkoutApplication.Modules.Users.Entities;
using WorkoutApplication.Modules.Users.Features.GetUser;

namespace WorkoutApplication.Modules.Users.Features.CreateUser
{
    public class CreateUser
    {
        public static CreateUserResponse Handle(CreateUserRequest request)
        {


            var hashedPassword = new PasswordHasher<CreateUserRequest>().HashPassword(request, request.Password);

            return new CreateUserResponse(
                request.FirstName, request.LastName, request.Email, hashedPassword
            );
        }
    }
}
