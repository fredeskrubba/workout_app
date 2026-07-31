using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using WorkoutApplication.Modules.Routines.Features.AddExerciseToRoutine;
using WorkoutApplication.Modules.Routines.Features.GetAllRoutineExercises;
using WorkoutApplication.Shared.Data;
using WorkoutApplication.Shared.Entities;

namespace WorkoutApplication.Modules.Routines.Test.Features
{
    public class GetAllRoutineExercisesTest
    {
        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenGettingExercises()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<WorkoutApplicationDBContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

            var context = new WorkoutApplicationDBContext(options);
            var exercise = new Exercise()
            {
                Name = "Jumping jacks",
                Description = "Basic exercise",
                ExerciseType = Shared.Enums.ExerciseType.Bodyweight
            };
            context.Exercises.Add(exercise);

            User user = new("Test", "User", "testuser@gmail.com");
            context.Users.Add(user);

            await context.SaveChangesAsync();

            Routine routine = new()
            {
                Title = "Test Routine",
                UserId = user.UserId
            };
            context.Routines.Add(routine);
            await context.SaveChangesAsync();

            var routineExercise = new RoutineExercise
            {
                RoutineId = routine.RoutineId,
                ExerciseId = exercise.Id,
                Reps = 10,
                Sets = 3,
                Weight = 0
            };
            context.RoutineExercises.Add(routineExercise);
            await context.SaveChangesAsync();

            GetAllRoutineExercises Handler = new(context);
            GetAllRoutineExercisesRequest request = new();

            //Act
            var result = await Handler.Handle(request, 1, 1);

            //Assert
            result.IsSuccess.Should().BeTrue();

        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenRoutineDoesNotExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<WorkoutApplicationDBContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

            var context = new WorkoutApplicationDBContext(options);

            User user = new("Test", "User", "testuser2@gmail.com");
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var handler = new GetAllRoutineExercises(context);
            var request = new GetAllRoutineExercisesRequest();

            // Act
            var result = await handler.Handle(request, 9999, user.UserId);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be("Routine not found");
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenRoutineBelongsToAnotherUser()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<WorkoutApplicationDBContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

            var context = new WorkoutApplicationDBContext(options);

            User owner = new("Owner", "User", "owner@gmail.com");
            context.Users.Add(owner);
            User caller = new("Caller", "User", "caller@gmail.com");
            context.Users.Add(caller);
            await context.SaveChangesAsync();

            var routine = new Routine { Title = "Owner Routine", UserId = owner.UserId };
            context.Routines.Add(routine);
            await context.SaveChangesAsync();

            var handler = new GetAllRoutineExercises(context);
            var request = new GetAllRoutineExercisesRequest();

            // Act
            var result = await handler.Handle(request, routine.RoutineId, caller.UserId);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be("Routine not found");
        }
    }
}
