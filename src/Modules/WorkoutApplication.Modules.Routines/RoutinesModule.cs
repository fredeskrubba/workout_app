using Microsoft.Extensions.DependencyInjection;
using WorkoutApplication.Modules.Routines.Features.AddExerciseToRoutine;
using WorkoutApplication.Modules.Routines.Features.CreateRoutine;
using WorkoutApplication.Modules.Routines.Features.GetAllRoutineExercises;

namespace WorkoutApplication.Modules.Routines
{
    public static class RoutinesModule
    {
        public static IServiceCollection AddRoutinesModule(this IServiceCollection services)
        {
            services.AddScoped<CreateRoutine>();
            services.AddScoped<AddExerciseToRoutine>();
            services.AddScoped<GetAllRoutineExercises>();
            return services;
        }
    }
}
