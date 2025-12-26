using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using PracticeTracker.Domain.Entities;
using static System.Net.Mime.MediaTypeNames;

namespace PracticeTracker.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<InternshipVacancy> InternshipVacancies { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<PracticeJournal> PracticeJournals { get; set; }
        public DbSet<JournalEntry> JournalEntries { get; set; }
        public DbSet<FinalReport> FinalReports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Конфигурация User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
                entity.Property(e => e.PasswordHash).IsRequired();
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.UserType).IsRequired().HasConversion<string>();
                entity.HasIndex(e => e.Email).IsUnique();
            });

            // Конфигурация Student
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.StudentId);
                entity.HasOne(e => e.User)
                    .WithOne(u => u.Student)
                    .HasForeignKey<Student>(e => e.StudentId);

                entity.HasOne(e => e.Group)
                    .WithMany(g => g.Students)
                    .HasForeignKey(e => e.GroupId);
            });

            // Конфигурация Application
            modelBuilder.Entity<Application>(entity =>
            {
                entity.Property(e => e.ApplicationStatus)
                    .HasConversion<string>()
                    .HasDefaultValue("pending");

                entity.HasOne(e => e.Student)
                    .WithMany(s => s.Applications)
                    .HasForeignKey(e => e.StudentId);

                entity.HasOne(e => e.Vacancy)
                    .WithMany(v => v.Applications)
                    .HasForeignKey(e => e.VacancyId);
            });
        }
    }
}