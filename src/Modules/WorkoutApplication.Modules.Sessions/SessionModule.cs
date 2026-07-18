using Microsoft.Extensions.DependencyInjection;
using WorkoutApplication.Modules.Sessions.Features.CreateSession;
using WorkoutApplication.Modules.Sessions.Features.DeleteSession;
using WorkoutApplication.Modules.Sessions.Features.GetAllUserSessions;

namespace WorkoutApplication.Modules.Sessions
{
    public static class SessionModule
    {
        public static IServiceCollection AddWorkoutSessionModule(this IServiceCollection services)
        {
            services.AddScoped<CreateSession>();
            services.AddScoped<DeleteSession>();
            services.AddScoped<GetAllUserSessions>();

            return services;
        }
    }
}
