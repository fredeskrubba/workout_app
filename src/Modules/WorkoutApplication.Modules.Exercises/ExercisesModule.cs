using Microsoft.Extensions.DependencyInjection;
using WorkoutApplication.Modules.Exercises.Features.GetAllExercises;

namespace WorkoutApplication.Modules.Exercises
{
    public static class ExercisesModule
    {
        public static IServiceCollection AddExercisesModule(this IServiceCollection services)
        {
            services.AddScoped<GetAllExercises>();
            return services;
        }
    }
}
