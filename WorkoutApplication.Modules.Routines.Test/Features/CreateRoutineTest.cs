using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using WorkoutApplication.Modules.Routines.Features.CreateRoutine;
using WorkoutApplication.Shared.Data;
using WorkoutApplication.Shared.Entities;
using WorkoutApplication.Shared.Results;

namespace WorkoutApplication.Modules.Routines.Test.Features
{
    public class CreateRoutineTest
    {
        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenUserDoesntExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<WorkoutApplicationDBContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

            var context = new WorkoutApplicationDBContext(options);

         
            await context.SaveChangesAsync();

            CreateRoutine Handler = new CreateRoutine(context);
            CreateRoutineRequest request = new CreateRoutineRequest("test routine");

            //Act
            var result = await Handler.Handle(request, 22);

            //Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be("User not found");
        }

        
    }
}
