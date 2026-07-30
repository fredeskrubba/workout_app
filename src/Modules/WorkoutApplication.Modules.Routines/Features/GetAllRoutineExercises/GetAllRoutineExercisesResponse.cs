using System.Collections.Generic;
using WorkoutApplication.Shared.Enums;

namespace WorkoutApplication.Modules.Routines.Features.GetAllRoutineExercises
{
    public record GetAllRoutineExercisesResponse(IEnumerable<RoutineExerciseDto> Exercises);
    
    public record RoutineExerciseDto(
        int Id,
        int RoutineId,
        int Sets,
        int Reps,
        int? SeatSetting,
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
