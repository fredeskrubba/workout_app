using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WorkoutApplication.Modules.Users.Entities;

namespace WorkoutApplication.Modules.Users.Helpers
{
    public class CreateToken
    {
        private readonly IConfiguration _configuration;

        public CreateToken(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Create(User user)
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
