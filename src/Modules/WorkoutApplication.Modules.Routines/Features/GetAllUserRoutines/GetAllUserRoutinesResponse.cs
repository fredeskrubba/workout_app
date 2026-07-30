using WorkoutApplication.Shared.Entities;
using System.Collections.Generic;

namespace WorkoutApplication.Modules.Routines.Features.GetAllUserRoutines;

public record GetAllUserRoutinesResponse(IEnumerable<Routine> Routines);
