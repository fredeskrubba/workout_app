using Microsoft.EntityFrameworkCore;
using WorkoutApplication.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorkoutApplication.Shared.Data
{
    public class WorkoutApplicationDBContext(DbContextOptions<WorkoutApplicationDBContext> options) : DbContext(options)
    {
        public DbSet<WorkoutSession> WorkoutSessions { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<WorkoutSession>()
                .HasKey(x => x.SessionId);

            modelBuilder.Entity<WorkoutSession>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(w => w.UserId);

            modelBuilder.Entity<User>()
                .ToTable("users");

            modelBuilder.Entity<User>()
                .HasKey(x => x.UserId);

            modelBuilder.Entity<User>()
                .Property(x => x.UserId)
                .HasColumnName("user_id");

            modelBuilder.Entity<User>()
                .Property(x => x.FirstName)
                .HasColumnName("first_name");

            modelBuilder.Entity<User>()
                .Property(x => x.LastName)
                .HasColumnName("last_name");

            modelBuilder.Entity<User>()
                .Property(x => x.Email)
                .HasColumnName("email");

            modelBuilder.Entity<User>()
                .Property(x => x.HashedPassword)
                .HasColumnName("hashed_password");
        }
    }
}
