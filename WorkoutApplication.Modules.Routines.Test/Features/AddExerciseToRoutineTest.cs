using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using WorkoutApplication.Modules.Routines.Features.AddExerciseToRoutine;
using WorkoutApplication.Shared.Data;
using WorkoutApplication.Shared.Entities;

namespace WorkoutApplication.Modules.Routines.Test.Features
{
    public class AddExerciseToRoutineTest
    {
        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenAddingExercise()
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

            User user = new("Test", "User", "testuser@gmail.com");
            context.Users.Add(user);

            Routine routine = new()
            {
                Title = "Test Routine",
                UserId = 1
            };
            context.Routines.Add(routine);

            await context.SaveChangesAsync();

            AddExerciseToRoutine Handler = new AddExerciseToRoutine(context);
            AddExerciseToRoutineRequest request = new AddExerciseToRoutineRequest(1, 8, 3, 10, 2);

            //Act
            var result = await Handler.Handle(request, 1, 1);

            //Assert
            result.IsSuccess.Should().BeTrue();
            
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenNoRoutineExists()
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

            User user = new("Test", "User", "testuser@gmail.com");
            context.Users.Add(user);


            await context.SaveChangesAsync();

            AddExerciseToRoutine Handler = new AddExerciseToRoutine(context);
            AddExerciseToRoutineRequest request = new AddExerciseToRoutineRequest(1, 8, 3, 10, 2);

            //Act
            var result = await Handler.Handle(request, 1, 1);

            //Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be("Routine not found");
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenNoExerciseExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<WorkoutApplicationDBContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

            var context = new WorkoutApplicationDBContext(options);
            Routine routine = new()
            {
                Title = "Test Routine",
                UserId = 1
            };
            context.Routines.Add(routine);

            User user = new("Test", "User", "testuser@gmail.com");
            context.Users.Add(user);


            await context.SaveChangesAsync();

            AddExerciseToRoutine Handler = new AddExerciseToRoutine(context);
            AddExerciseToRoutineRequest request = new AddExerciseToRoutineRequest(1, 8, 3, 10, 2);

            //Act
            var result = await Handler.Handle(request, 1, 1);

            //Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be("Exercise not found");
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenUserDoesntExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<WorkoutApplicationDBContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

            var context = new WorkoutApplicationDBContext(options);


         
            await context.SaveChangesAsync();

            AddExerciseToRoutine Handler = new AddExerciseToRoutine(context);
            AddExerciseToRoutineRequest request = new AddExerciseToRoutineRequest(1, 8, 3, 10, 2);

            //Act
            var result = await Handler.Handle(request, 1, 1);

            //Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be("User not found");
        }
    }
}
