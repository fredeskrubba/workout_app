using System;
using System.Collections.Generic;
using System.Text;

namespace WorkoutApplication.Modules.Routines.Features.AddExerciseToRoutine
{
    public record AddExerciseToRoutineRequest(int ExerciseId, int Reps, int Sets, double Weight, int SeatSetting);
}
