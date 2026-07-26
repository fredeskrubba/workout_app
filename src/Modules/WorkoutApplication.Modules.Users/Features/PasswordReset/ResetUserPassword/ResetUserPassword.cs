using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkoutApplication.Shared.Data;
using WorkoutApplication.Shared.Entities;
using WorkoutApplication.Shared.Results;

namespace WorkoutApplication.Modules.Users.Features.PasswordReset.ResetUserPassword
{
    public class ResetUserPassword
    {
        private readonly WorkoutApplicationDBContext _context;

        public ResetUserPassword(WorkoutApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Result<ResetUserPasswordResponse?>> Handle(ResetUserPasswordRequest request)
        {


            return Result<ResetUserPasswordResponse?>.Success(new ResetUserPasswordResponse());
        }
    }
}
