using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using WorkoutApplication.Modules.Users.Features.GetUser;
using WorkoutApplication.Shared.Data;
using WorkoutApplication.Shared.Entities;
using WorkoutApplication.Shared.Results;
namespace WorkoutApplication.Modules.Users.Test.Features
{
    public class GetUserTest
    {
        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenUserNotFound()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<WorkoutApplicationDBContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

            var context = new WorkoutApplicationDBContext(options);
            string email = "test@testmail.com";

            User user = new User("Test", "User", email);

            context.Users.Add(user);


            await context.SaveChangesAsync();

            GetUser Handler = new GetUser(context);
            GetUserRequest request = new GetUserRequest(11111);

            //Act
            var result = await Handler.Handle(request);

            //Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be("User not found");

        }

        [Fact]
        public async Task Handle_ShouldReturnEquals_WhenUserFound()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<WorkoutApplicationDBContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

            var context = new WorkoutApplicationDBContext(options);
            string email = "test@testmail.com";

            User user = new User("Test", "User", email);

            context.Users.Add(user);


            await context.SaveChangesAsync();

            GetUser Handler = new GetUser(context);
            GetUserRequest request = new GetUserRequest(1);

            //Act
            var result = await Handler.Handle(request);

            //Assert
            result.IsSuccess.Should().BeTrue();

            var foundUser = await context.Users
                .FirstOrDefaultAsync(u => u.UserId == user.UserId);

            result.Value.Id.Should().Be(user.UserId);
            result.Value.Email.Should().Be(user.Email);
            result.Value.FirstName.Should().Be(user.FirstName);

        }
    }
}
