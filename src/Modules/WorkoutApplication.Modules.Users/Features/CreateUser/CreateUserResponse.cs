using System;
using System.Collections.Generic;
using System.Text;
using WorkoutApplication.Modules.Users.Entities;

namespace WorkoutApplication.Modules.Users.Features.CreateUser
{
    public record CreateUserResponse(string FirstName, string LastName, string Email, string HashedPassword);

}
