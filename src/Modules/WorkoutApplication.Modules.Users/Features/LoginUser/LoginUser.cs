using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkoutApplication.Shared.Entities;
using WorkoutApplication.Modules.Users.Helpers;
using WorkoutApplication.Shared.Data;
using WorkoutApplication.Shared.Results;


namespace WorkoutApplication.Modules.Users.Features.LoginUser
{
    public class LoginUser
    {
        
        private readonly WorkoutApplicationDBContext _context;
        private readonly TokenHelper _tokenHelper;
        public LoginUser(WorkoutApplicationDBContext context, TokenHelper tokenHelper)
        {
           
            _context = context;
            _tokenHelper = tokenHelper;
        }
        public async Task<Result<LoginUserResponse>> Handle(LoginUserRequest request)
        {
            
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

            if(user is null)
            {
                return Result<LoginUserResponse>.Failure("User not found");
            }

            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.HashedPassword, request.Password) == PasswordVerificationResult.Failed)
            {
                return Result<LoginUserResponse>.Failure("Wrong password");
                
            }

            
            var refreshToken = _tokenHelper.GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return Result<LoginUserResponse>.Failure("Something went wrong, see error: " + ex.Message);
            }

            return Result<LoginUserResponse>.Success(new LoginUserResponse(_tokenHelper.CreateToken(user), refreshToken));
        }


         


        

    }
}
