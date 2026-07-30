using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WorkoutApplication.Shared.Entities
{
    [Table("routines")]
    public class Routine
    {
        [Column("routine_id")]
        public int RoutineId { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("UserId")]
        public int UserId { get; set; }
    }
}
