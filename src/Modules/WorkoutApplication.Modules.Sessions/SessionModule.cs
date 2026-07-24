using Microsoft.Extensions.DependencyInjection;
using WorkoutApplication.Modules.Sessions.Features.CreateSession;
using WorkoutApplication.Modules.Sessions.Features.DeleteSession;
using WorkoutApplication.Modules.Sessions.Features.GetAllUserSessions;
using WorkoutApplication.Modules.Sessions.Features.AddExerciseToSession;
using WorkoutApplication.Modules.Sessions.Features.GetAllSessionExercises;
namespace WorkoutApplication.Modules.Sessions
{
    public static class SessionModule
    {
        public static IServiceCollection AddWorkoutSessionModule(this IServiceCollection services)
        {
            services.AddScoped<CreateSession>();
            services.AddScoped<DeleteSession>();
            services.AddScoped<GetAllUserSessions>();
            services.AddScoped<AddExerciseToSession>();
            services.AddScoped<GetAllSessionExercises>();
            return services;
        }
    }
}
