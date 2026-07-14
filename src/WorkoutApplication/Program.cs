using Microsoft.EntityFrameworkCore;
using WorkoutApplication.Modules.Users;
using WorkoutApplication.Modules.Users.Data;
using WorkoutApplication.Modules.Users.Features.CreateUser;
using WorkoutApplication.Modules.Users.Features.DeleteUser;
using WorkoutApplication.Modules.Users.Features.GetUser;
using WorkoutApplication.Modules.Users.Features.LoginUser;
using WorkoutApplication.Modules.Users.Features.UpdateUserPassword;
using WorkoutApplication.Modules.Users.Helpers;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<LoginUser>();
builder.Services.AddScoped<GetUser>();
builder.Services.AddScoped<CreateUser>();
builder.Services.AddScoped<DeleteUser>();
builder.Services.AddScoped<UpdateUserPassword>();
builder.Services.AddSingleton<CreateToken>();

var connectionString = builder.Configuration
    .GetConnectionString("DefaultConnection");

Console.WriteLine(connectionString);


builder.Services.AddUsersModule(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGetUserEndpoint();
app.MapCreateUserEndpoint();
app.MapLoginUserEndpoint();
app.MapDeleteUserEndpoint();
app.MapUpdateUserEndpointEndpoint();

app.Run();
