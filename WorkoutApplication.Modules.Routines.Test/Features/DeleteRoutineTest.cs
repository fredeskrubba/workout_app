using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using WorkoutApplication.Modules.Routines.Features.DeleteRoutine;
using WorkoutApplication.Shared.Data;
using WorkoutApplication.Shared.Entities;
using WorkoutApplication.Shared.Results;

namespace WorkoutApplication.Modules.Routines.Test.Features
{
    public class DeleteRoutineTest
    {
        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenUserDoesntOwnRoutine()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<WorkoutApplicationDBContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

            var context = new WorkoutApplicationDBContext(options);

            context.Exercises.Add(new Exercise()
            {
                Name = "Jumping jacks",
                Description = "Basic exercise",
                ExerciseType = Shared.Enums.ExerciseType.Bodyweight
            });

            string loggedInUserId = "3333";
            User user = new("Test", "User", "testuser@gmail.com");
            context.Users.Add(user);

            Routine routine = new()
            {
                Title = "Test Routine",
                UserId = 1
            };
            context.Routines.Add(routine);

            await context.SaveChangesAsync();

            DeleteRoutine Handler = new DeleteRoutine(context);
            DeleteRoutineRequest request = new DeleteRoutineRequest(1, loggedInUserId);

            //Act
            var result = await Handler.Handle(request);

            //Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be("No routine with the supplied id found for the logged in user with id: " + loggedInUserId);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenRoutineDoesntExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<WorkoutApplicationDBContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

            var context = new WorkoutApplicationDBContext(options);

            context.Exercises.Add(new Exercise()
            {
                Name = "Jumping jacks",
                Description = "Basic exercise",
                ExerciseType = Shared.Enums.ExerciseType.Bodyweight
            });

            string loggedInUserId = "3333";
            User user = new("Test", "User", "testuser@gmail.com");
            context.Users.Add(user);

            

            await context.SaveChangesAsync();

            DeleteRoutine Handler = new DeleteRoutine(context);
            DeleteRoutineRequest request = new DeleteRoutineRequest(2, loggedInUserId);

            //Act
            var result = await Handler.Handle(request);

            //Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be("Routine not found");
        }
    }
}
