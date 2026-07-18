using Microsoft.Extensions.DependencyInjection;
using WorkoutApplication.Modules.Users.Features.CreateUser;
using WorkoutApplication.Modules.Users.Features.DeleteUser;
using WorkoutApplication.Modules.Users.Features.GetUser;
using WorkoutApplication.Modules.Users.Features.LoginUser;
using WorkoutApplication.Modules.Users.Features.UpdateUserPassword;
using WorkoutApplication.Modules.Users.Features.UpdateRefreshToken;

namespace WorkoutApplication.Modules.Users
{
    public static class UsersModule
    {
        public static IServiceCollection AddUsersModule(this IServiceCollection services)
        {
            services.AddScoped<LoginUser>();
            services.AddScoped<GetUser>();
            services.AddScoped<CreateUser>();
            services.AddScoped<DeleteUser>();
            services.AddScoped<UpdateUserPassword>();
            services.AddScoped<UpdateRefreshToken>();

            return services;
        }
    }
}
