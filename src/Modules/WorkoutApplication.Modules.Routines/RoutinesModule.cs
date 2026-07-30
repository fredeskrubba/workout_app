using Microsoft.Extensions.DependencyInjection;
using WorkoutApplication.Modules.Routines.Features.AddExerciseToRoutine;
using WorkoutApplication.Modules.Routines.Features.CreateRoutine;
using WorkoutApplication.Modules.Routines.Features.GetAllRoutineExercises;
using WorkoutApplication.Modules.Routines.Features.DeleteRoutine;
using WorkoutApplication.Modules.Routines.Features.GetAllUserRoutines;

namespace WorkoutApplication.Modules.Routines
{
    public static class RoutinesModule
    {
        public static IServiceCollection AddRoutinesModule(this IServiceCollection services)
        {
            services.AddScoped<CreateRoutine>();
            services.AddScoped<AddExerciseToRoutine>();
            services.AddScoped<GetAllRoutineExercises>();
            services.AddScoped<DeleteRoutine>();
            services.AddScoped<GetAllUserRoutines>();
            return services;
        }
    }
}
