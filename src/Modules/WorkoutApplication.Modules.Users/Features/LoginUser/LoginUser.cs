using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WorkoutApplication.Modules.Users.Data;
using WorkoutApplication.Modules.Users.Entities;
using WorkoutApplication.Shared.Results;
namespace WorkoutApplication.Modules.Users.Features.LoginUser
{
    public class LoginUser
    {
        private readonly IConfiguration _configuration;
        private readonly UserDBContext _context;
        public LoginUser(IConfiguration configuration, UserDBContext context)
        {
            _configuration = configuration;
            _context = context;
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

            return Result<LoginUserResponse>.Success(new LoginUserResponse(CreateToken(user)));
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName)
            };

            var key = new SymmetricSecurityKey(
                   Encoding.UTF8.GetBytes(_configuration["AppSettings:Token"]!)
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                 issuer: _configuration["AppSettings:Issuer"],
                 audience: _configuration["AppSettings:Audience"],
                 claims: claims,
                 expires: DateTime.UtcNow.AddDays(1),
                 signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
