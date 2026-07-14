using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WorkoutApplication.Modules.Users.Data;
using WorkoutApplication.Modules.Users.Entities;
using WorkoutApplication.Shared.Results;
using WorkoutApplication.Modules.Users.Helpers;


namespace WorkoutApplication.Modules.Users.Features.LoginUser
{
    public class LoginUser
    {
        
        private readonly UserDBContext _context;
        private readonly TokenHelper _tokenHelper;
        public LoginUser(UserDBContext context, TokenHelper tokenHelper)
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

            await _context.SaveChangesAsync();

            return Result<LoginUserResponse>.Success(new LoginUserResponse(_tokenHelper.CreateToken(user), refreshToken));
        }


         


        

    }
}
