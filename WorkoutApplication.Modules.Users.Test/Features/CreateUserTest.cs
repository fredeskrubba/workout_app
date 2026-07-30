using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using WorkoutApplication.Modules.Users.Features.CreateUser;
using WorkoutApplication.Shared.Data;
using WorkoutApplication.Shared.Entities;
using WorkoutApplication.Shared.Results;

namespace WorkoutApplication.Modules.Users.Test.Features
{
    public class CreateUserTest
    {
        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenEmailAlreadyExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<WorkoutApplicationDBContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

            var context = new WorkoutApplicationDBContext(options);
            string email = "test@testmail.com";

            context.Users.Add(new User("Test", "User", email ));

            await context.SaveChangesAsync();

            CreateUser Handler = new CreateUser(context);
            CreateUserRequest request = new CreateUserRequest("test", "user", email, "password");

            //Act
            var result = await Handler.Handle(request);

            //Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be("Email already in use");
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenEmailIsntValid()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<WorkoutApplicationDBContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

            var context = new WorkoutApplicationDBContext(options);

            string email = "test.com";

            context.Users.Add(new User("Test", "User", email));

            await context.SaveChangesAsync();

            CreateUser Handler = new CreateUser(context);
            CreateUserRequest request = new CreateUserRequest("test", "user", email, "password");

            //Act
            var result = await Handler.Handle(request);

            //Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be("Email not in a valid format");
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenHashedPasswordIsStored()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<WorkoutApplicationDBContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

            var context = new WorkoutApplicationDBContext(options);

            string email = "test@user123.com";
            string password = "password";

            context.Users.Add(new User("Test", "User", email));

            await context.SaveChangesAsync();

            CreateUser Handler = new CreateUser(context);
            CreateUserRequest request = new CreateUserRequest("test", "user", "test@user1234.com", password);

            //Act
            var result = await Handler.Handle(request);

            //Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.HashedPassword.Should().NotBe(password);
            result.Value.HashedPassword.Should().NotBeNullOrEmpty();
        }
    }
}
