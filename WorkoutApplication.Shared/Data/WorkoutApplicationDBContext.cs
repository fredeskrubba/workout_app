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
        
        public DbSet<Exercise> Exercises { get; set; }
        
        public DbSet<MuscleGroup> MuscleGroups { get; set; }

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
            
            modelBuilder.Entity<Exercise>()
                .HasKey(x => x.Id);
            
            modelBuilder.Entity<MuscleGroup>()
                .HasKey(x => x.Id);
            
            modelBuilder.Entity<ExerciseMuscleGroup>()
                .HasKey(x => new { x.ExerciseId, x.MuscleGroupId });

            modelBuilder.Entity<ExerciseMuscleGroup>()
                .HasOne<Exercise>()
                .WithMany()
                .HasForeignKey(x => x.ExerciseId);

            modelBuilder.Entity<ExerciseMuscleGroup>()
                .HasOne<MuscleGroup>()
                .WithMany()
                .HasForeignKey(x => x.MuscleGroupId);
        }
    }
}
