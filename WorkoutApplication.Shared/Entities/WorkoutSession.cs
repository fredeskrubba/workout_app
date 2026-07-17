using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WorkoutApplication.Shared.Entities
{
    [Table("workout_sessions")]
    public class WorkoutSession
    {
        [Column("session_id")]
        public int SessionId { get; set; }
        [Column("rating")]
        public int Rating { get; set; }
        [Column("date")]

        public DateTime Date { get; set; }

        [Column("duration_seconds")]
        public int DurationSeconds { get; set; }
        [Column("user_id")]

        public int UserId { get; set; }
    }
}
