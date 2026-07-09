using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkoutApplication.Modules.Users.Data;

namespace WorkoutApplication.Modules.Users
{
    public static class UsersModule
    {
        public static IServiceCollection AddUsersModule(
        this IServiceCollection services,
        IConfiguration configuration)
        {


            services.AddDbContext<UserDBContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection")
                ));

            return services;
        }
    }
}
