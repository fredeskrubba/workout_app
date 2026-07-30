using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using WorkoutApplication.Modules.Users.Features.DeleteUser;
using WorkoutApplication.Shared.Data;
using WorkoutApplication.Shared.Entities;
using WorkoutApplication.Shared.Results;

namespace WorkoutApplication.Modules.Users.Test.Features
{
    public class DeleteUserTest
    {
        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenDeletingUser()
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

            DeleteUser Handler = new DeleteUser(context);
            DeleteUserRequest request = new DeleteUserRequest(user.UserId, user.UserId.ToString());

            //Act
            var result = await Handler.Handle(request);

            //Assert
            result.IsSuccess.Should().BeTrue();
            
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenUserDeletedIsNotLoggedInUser()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<WorkoutApplicationDBContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

            var context = new WorkoutApplicationDBContext(options);
            string email = "test@testmail.com";

            User user = new User("Test", "User", email);
            User loggedInUser = new("Test", "User2", "test@testmail2.com");

            context.Users.Add(user);


            await context.SaveChangesAsync();

            DeleteUser Handler = new DeleteUser(context);
            DeleteUserRequest request = new DeleteUserRequest(user.UserId, loggedInUser.UserId.ToString());

            //Act
            var result = await Handler.Handle(request);

            //Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be("The logged in user does not have permission to delete this user");
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenUserDeleted()
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

            DeleteUser Handler = new DeleteUser(context);
            DeleteUserRequest request = new DeleteUserRequest(user.UserId, user.UserId.ToString());

            //Act
            var result = await Handler.Handle(request);

            //Assert
            result.IsSuccess.Should().BeTrue();

            var deletedUser = await context.Users
                .FirstOrDefaultAsync(u => u.UserId == user.UserId);

            deletedUser.Should().BeNull();
        }

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

            DeleteUser Handler = new DeleteUser(context);
            DeleteUserRequest request = new DeleteUserRequest(23, "23");

            //Act
            var result = await Handler.Handle(request);

            //Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be("User not found");


        }
    }
}
