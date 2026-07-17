using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkoutApplication.Shared.Entities;
using WorkoutApplication.Shared.Results;
using WorkoutApplication.Shared.Data;

namespace WorkoutApplication.Modules.Users.Features.CreateUser
{
    public class CreateUser
    {
        private readonly WorkoutApplicationDBContext _context;

        public CreateUser(WorkoutApplicationDBContext context)
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

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return Result<CreateUserResponse>.Failure("Something went wrong, see error: " + ex.Message);
            }

            CreateUserResponse createdUser = new(user.FirstName,
            user.LastName,
            user.Email,
            user.HashedPassword);

            return Result<CreateUserResponse>.Success(createdUser);
        }
    }
}
