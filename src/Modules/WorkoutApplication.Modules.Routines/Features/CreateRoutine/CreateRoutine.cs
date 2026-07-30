using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WorkoutApplication.Shared.Data;
using WorkoutApplication.Shared.Entities;
using WorkoutApplication.Shared.Results;

namespace WorkoutApplication.Modules.Routines.Features.CreateRoutine
{
    public class CreateRoutine
    {
        private readonly WorkoutApplicationDBContext _context;

        public CreateRoutine(WorkoutApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Result<CreateRoutineResponse>> Handle(CreateRoutineRequest request, int loggedInUserId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == loggedInUserId);

            if (user is null)
            {
                return Result<CreateRoutineResponse>.Failure("User not found");
            }

            Routine routine = new()
            {
                UserId = loggedInUserId,
                Title = request.Title
            };

            _context.Routines.Add(routine);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return Result<CreateRoutineResponse>.Failure("Something went wrong, see error: " + ex.Message);
            }

            CreateRoutineResponse response = new();

            return Result<CreateRoutineResponse>.Success(response);
        }
    }
}
