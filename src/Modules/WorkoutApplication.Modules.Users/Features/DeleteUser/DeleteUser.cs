using Microsoft.EntityFrameworkCore;
using WorkoutApplication.Shared.Data;
using WorkoutApplication.Shared.Results;

namespace WorkoutApplication.Modules.Users.Features.DeleteUser
{
   public class DeleteUser
    {
        private readonly WorkoutApplicationDBContext _context;

        public DeleteUser(WorkoutApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Result<DeleteUserResponse>> Handle(DeleteUserRequest request)
        {
           
            var user = await _context.Users.FirstOrDefaultAsync( x => x.UserId == request.UserId);
            
            if (user is null)
            {
                return Result<DeleteUserResponse>.Failure("User not found");
            }

            if (Int32.Parse(request.LoggedInUserId) != user.UserId)
            {
                return Result<DeleteUserResponse>.Failure("The logged in user does not have permission to delete this user");
            }

            _context.Users.Remove(user);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return Result<DeleteUserResponse>.Failure("Something went wrong, see error: " + ex.Message);
            }

            return Result<DeleteUserResponse>.Success(new DeleteUserResponse(
                "User deleted"
            ));
        }
    }
}
