using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkoutApplication.Shared.Entities;
using WorkoutApplication.Shared.Data;
using WorkoutApplication.Shared.Results;


namespace WorkoutApplication.Modules.Users.Features.UpdateUserPassword
{
    public class UpdateUserPassword
    {
        private readonly WorkoutApplicationDBContext _context;
        

        public UpdateUserPassword(WorkoutApplicationDBContext context)
        {
            _context = context;
           
        }

        public async Task<Result<UpdateUserPasswordResponse?>> Handle(UpdateUserPasswordRequest request, string loggedInUserId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
            int userId = int.Parse(loggedInUserId);

            if (user is null)
            {
                return Result<UpdateUserPasswordResponse?>.Failure("User not found");
            }

            if(user.UserId != userId)
            {
                return Result<UpdateUserPasswordResponse?>.Failure("Email doesnt belong to the logged in user");
            }

            var newHashedPassword = new PasswordHasher<User>().HashPassword(user, request.NewPassword);

            user.HashedPassword = newHashedPassword;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return Result<UpdateUserPasswordResponse>.Failure("Something went wrong, see error: " + ex.Message);
            }

            return Result<UpdateUserPasswordResponse?>.Success(new UpdateUserPasswordResponse(newHashedPassword));

        }
    }
}
