using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using WorkoutApplication.Modules.Exercises.Features.GetAllExercises;
using WorkoutApplication.Shared.Data;
using WorkoutApplication.Shared.Entities;

namespace WorkoutApplication.Modules.Exercises.Test.Features
{
    public class GetAllExercisesTest
    {
        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenNoExercisesFound()
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

            GetAllExercises Handler = new GetAllExercises(context);
            GetAllExercisesRequest request = new GetAllExercisesRequest();

            //Act
            var result = await Handler.Handle(request);

            //Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be("No exercises found");
        }
    }
}
