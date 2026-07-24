using System;
using System.Collections.Generic;
using System.Text;
using WorkoutApplication.Shared.Enums;
using WorkoutApplication.Shared.Entities;

namespace WorkoutApplication.Modules.Exercises.Features.GetAllExercises
{
    public record GetAllExercisesResponse(IEnumerable<Exercise> Exercises);

    public record ExerciseDto(
       int Id,
       string Name,
       string Description,
       ExerciseType ExerciseType,
       ExerciseDto Exercise
   );

    public record MuscleGroupDto(
        int Id,
        string Name

    );

}
