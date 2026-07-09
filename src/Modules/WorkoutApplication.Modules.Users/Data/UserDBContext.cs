using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WorkoutApplication.Modules.Users.Entities;

namespace WorkoutApplication.Modules.Users.Data
{
    public class UserDBContext(DbContextOptions<UserDBContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
