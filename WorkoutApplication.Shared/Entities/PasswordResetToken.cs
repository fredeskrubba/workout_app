using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WorkoutApplication.Shared.Entities
{
    [Table("password_reset_tokens")]
    public class PasswordResetToken
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("hashed_token")]

        public string TokenHash { get; set; }
        [Column("expires_at")]

        public DateTime ExpiresAt { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("is_used")]

        public bool IsUsed { get; set; } = false;

        public User User { get; set; }
    }
}
