using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WorkoutApplication.Modules.Routines.Features.GetAllUserRoutines;
using WorkoutApplication.Shared.Data;
using WorkoutApplication.Shared.Entities;
using Xunit;

namespace WorkoutApplication.Modules.Routines.Test.Features
{
    public class GetAllUserRoutinesTest
    {
        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenUserHasRoutines()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<WorkoutApplicationDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new WorkoutApplicationDBContext(options);

            var user = new User("First", "Last", "user@example.com");
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var routine1 = new Routine { Title = "Routine 1", UserId = user.UserId };
            var routine2 = new Routine { Title = "Routine 2", UserId = user.UserId };
            context.Routines.AddRange(routine1, routine2);
            await context.SaveChangesAsync();

            var handler = new GetAllUserRoutines(context);
            var request = new GetAllUserRoutinesRequest(user.UserId.ToString());

            // Act
            var result = await handler.Handle(request);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            var routines = result.Value!.Routines.ToList();
            routines.Should().HaveCount(2);
            routines.Select(r => r.Title).Should().Contain(new[] { "Routine 1", "Routine 2" });
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenUserHasNoRoutines()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<WorkoutApplicationDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new WorkoutApplicationDBContext(options);

            var user = new User("First", "Last", "empty@example.com");
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var handler = new GetAllUserRoutines(context);
            var request = new GetAllUserRoutinesRequest(user.UserId.ToString());

            // Act
            var result = await handler.Handle(request);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be("No routines found for user with userid of " + request.LoggedInUserId);
        }
    }
}
