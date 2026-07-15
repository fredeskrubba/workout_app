using Microsoft.EntityFrameworkCore;
using WorkoutApplication.Shared.Data;

namespace WorkoutApplication.Modules.Users.Features.DeleteUser
{
   public class DeleteUser
    {
        private readonly WorkoutApplicationDBContext _context;

        public DeleteUser(WorkoutApplicationDBContext context)
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
