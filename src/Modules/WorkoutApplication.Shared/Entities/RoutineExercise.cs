using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WorkoutApplication.Shared.Entities
{
    [Table("routine_exercises")]
    public class RoutineExercise
    {
        [Column("routine_exercise_id")]
        public int RoutineExerciseId { get; set; }
        [Column("reps")]
        public int Reps { get; set; }
        [Column("routine_id")]

        public int RoutineId { get; set; }

        [Column("exercise_id")]
        public int ExerciseId { get; set; }

        [Column("weight")]
        public double Weight { get; set; }

        [Column("sets")]
        public int Sets { get; set; }

        [Column("seat_setting")]
        public int? SeatSetting { get; set; }

        public Exercise Exercise { get; set; } = null!;
    }
}
