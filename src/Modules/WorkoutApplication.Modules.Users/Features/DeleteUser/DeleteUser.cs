using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using WorkoutApplication.Modules.Users.Data;
using Microsoft.EntityFrameworkCore;
using WorkoutApplication.Modules.Users.Entities;
using WorkoutApplication.Modules.Users.Features.CreateUser;

namespace WorkoutApplication.Modules.Users.Features.DeleteUser
{
   public class DeleteUser
    {
        private readonly UserDBContext _context;

        public DeleteUser(UserDBContext context)
        {
            _context = context;
        }

        public async Task<DeleteUserResponse?> Handle(DeleteUserRequest request)
        {
           
            var user = await _context.Users.FirstOrDefaultAsync( x => x.UserId == request.UserId);

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();

            return new DeleteUserResponse(
                "User deleted"
            );
        }
    }
}
