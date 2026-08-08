using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using WorkoutApplication.Modules.Users;
using WorkoutApplication.Modules.Sessions;
using WorkoutApplication.Modules.Users.Features.CreateUser;
using WorkoutApplication.Modules.Users.Features.DeleteUser;
using WorkoutApplication.Modules.Users.Features.GetUser;
using WorkoutApplication.Modules.Users.Features.LoginUser;
using WorkoutApplication.Modules.Users.Features.UpdateUserPassword;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WorkoutApplication.Modules.Sessions.Features.AddExerciseToSession;
using WorkoutApplication.Modules.Sessions.Features.CreateSession;
using WorkoutApplication.Modules.Sessions.Features.DeleteSession;
using WorkoutApplication.Modules.Sessions.Features.GetAllUserSessions;
using WorkoutApplication.Modules.Users.Features.UpdateRefreshToken;
using WorkoutApplication.Modules.Users.Helpers;
using WorkoutApplication.Shared.Data;
using WorkoutApplication.Modules.Sessions.Features.GetAllSessionExercises;
using WorkoutApplication.Modules.Users.Features.PasswordReset.ResetUserPassword;
using WorkoutApplication.Modules.Users.Features.PasswordReset.ForgotUserPassword;
using WorkoutApplication.Modules.Exercises;
using WorkoutApplication.Modules.Exercises.Features.GetAllExercises;
using WorkoutApplication.Shared.Services.Email;
using WorkoutApplication.Modules.Routines.Features.CreateRoutine;
using WorkoutApplication.Modules.Routines.Features.AddExerciseToRoutine;
using WorkoutApplication.Modules.Routines;
using WorkoutApplication.Modules.Routines.Features.GetAllRoutineExercises;
using WorkoutApplication.Modules.Routines.Features.DeleteRoutine;
using WorkoutApplication.Modules.Routines.Features.GetAllUserRoutines;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddUsersModule();
builder.Services.AddWorkoutSessionModule();
builder.Services.AddExercisesModule();
builder.Services.AddRoutinesModule();
builder.Services.AddScoped<IEmailService, EmailService>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["AppSettings:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["AppSettings:Audience"],
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"])),
        ValidateIssuerSigningKey = true

    };
});

builder.Services.AddSingleton<TokenHelper>();
builder.Services.AddDbContext<WorkoutApplicationDBContext>(options =>
            options.UseNpgsql(
                builder.Configuration.GetConnectionString("DefaultConnection")
            ));

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:8100")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("Frontend");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


var api = app.MapGroup("/api");

api.MapGetUserEndpoint();
api.MapCreateUserEndpoint();
api.MapLoginUserEndpoint();
api.MapDeleteUserEndpoint();
api.MapUpdateUserEndpointEndpoint();
api.MapUpdateRefreshTokenEndpoint();
api.MapCreateSessionEndpoint();
api.MapDeleteSessionEndpoint();
api.MapGetAllUserSessionsEndpoint();
api.MapAddExerciseToSessionEndpoint();
api.MapGetAllSessionExercisesEndpoint();
api.MapForgotUserPasswordEndpoint();
api.MapResetUserPasswordEndpoint();
api.MapGetAllExercisesEndpoint();
api.MapCreateRoutineEndpoint();
api.MapAddExerciseToRoutineEndpoint();
api.MapGetAllRoutineExercisesEndpoint();
api.MapDeleteRoutineEndpoint();
api.MapGetAllUserRoutinesEndpoint();


app.Run();
