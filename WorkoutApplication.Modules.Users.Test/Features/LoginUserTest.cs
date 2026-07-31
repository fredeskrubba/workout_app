using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using WorkoutApplication.Modules.Users.Features.LoginUser;
using WorkoutApplication.Modules.Users.Helpers;
using WorkoutApplication.Shared.Data;
using WorkoutApplication.Shared.Entities;

namespace WorkoutApplication.Modules.Users.Test.Features;

public class LoginUserTest
{
    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenUserNotFound()
    {
        var options = new DbContextOptionsBuilder<WorkoutApplicationDBContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new WorkoutApplicationDBContext(options);

        var inMemorySettings = new Dictionary<string, string>
        {
            { "AppSettings:Token", "testtoken" },
            { "AppSettings:Issuer", "issuer" },
            { "AppSettings:Audience", "audience" }
        };

        var configuration = new ConfigurationBuilder().AddInMemoryCollection(inMemorySettings).Build();
        var tokenHelper = new TokenHelper(configuration);

        var handler = new LoginUser(context, tokenHelper);
        var request = new LoginUserRequest("notfound@test.com", "password");

        var result = await handler.Handle(request);

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be("User not found");
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenWrongPassword()
    {
        var options = new DbContextOptionsBuilder<WorkoutApplicationDBContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new WorkoutApplicationDBContext(options);

        string email = "test@user.com";
        var user = new User("Test", "User", email);
        // set hashed password for "correctpassword"
        user.HashedPassword = new PasswordHasher<User>().HashPassword(user, "correctpassword");

        context.Users.Add(user);
        await context.SaveChangesAsync();

        var inMemorySettings = new Dictionary<string, string>
        {
            { "AppSettings:Token", "testtoken" },
            { "AppSettings:Issuer", "issuer" },
            { "AppSettings:Audience", "audience" }
        };

        var configuration = new ConfigurationBuilder().AddInMemoryCollection(inMemorySettings).Build();
        var tokenHelper = new TokenHelper(configuration);

        var handler = new LoginUser(context, tokenHelper);
        var request = new LoginUserRequest(email, "wrongpassword");

        var result = await handler.Handle(request);

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be("Wrong password");
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenCredentialsAreValid()
    {
        var options = new DbContextOptionsBuilder<WorkoutApplicationDBContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new WorkoutApplicationDBContext(options);

        string email = "test@user.com";
        var user = new User("Test", "User", email);
        string password = "password";
        user.HashedPassword = new PasswordHasher<User>().HashPassword(user, password);

        context.Users.Add(user);
        await context.SaveChangesAsync();

        var inMemorySettings = new Dictionary<string, string>
        {
            { "AppSettings:Token", "testtoken11111111111111111111111111111111111111111111113233222222222222222222222222" },
            { "AppSettings:Issuer", "issuer" },
            { "AppSettings:Audience", "audience" }
        };

        var configuration = new ConfigurationBuilder().AddInMemoryCollection(inMemorySettings).Build();
        var tokenHelper = new TokenHelper(configuration);

        var handler = new LoginUser(context, tokenHelper);
        var request = new LoginUserRequest(email, password);

        var result = await handler.Handle(request);

        result.IsSuccess.Should().BeTrue();
        result.Value.RefreshToken.Should().NotBeNullOrEmpty();

        var updatedUser = await context.Users.FirstOrDefaultAsync(u => u.Email == email);
        updatedUser.Should().NotBeNull();
        updatedUser!.RefreshToken.Should().NotBeNullOrEmpty();
        updatedUser.RefreshTokenExpiryTime.Should().NotBeNull();
        // Response refresh token should equal the stored refresh token
        result.Value.RefreshToken.Should().Be(updatedUser.RefreshToken);
    }
}
