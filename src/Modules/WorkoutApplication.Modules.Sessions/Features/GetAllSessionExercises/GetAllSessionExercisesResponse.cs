using System;
using System.Collections.Generic;
using System.Text;
using WorkoutApplication.Shared.Entities;
using WorkoutApplication.Shared.Enums;

namespace WorkoutApplication.Modules.Sessions.Features.GetAllSessionExercises
{
    public record GetAllSessionExercisesResponse(IEnumerable<SessionExerciseDto> Exercises);

    public record SessionExerciseDto(
        int Id,
        int SessionId,
        int Sets,
        int Reps,
        double Weight,
        ExerciseDto Exercise
    );

    public record ExerciseDto(
        int Id,
        string Name,
        string Description,
        ExerciseType ExerciseType
    );
}
