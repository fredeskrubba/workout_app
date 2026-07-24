using System;
using System.Collections.Generic;
using System.Text;

namespace WorkoutApplication.Modules.Sessions.Features.GetAllSessionExercises
{
    public record GetAllSessionExercisesRequest(string LoggedInUserId, int SessionId);
}
