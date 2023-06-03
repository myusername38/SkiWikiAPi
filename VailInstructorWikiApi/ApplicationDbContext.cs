using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using VailInstructorWikiApi.Models;

namespace VailInstructorWikiApi
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Area> Areas { get; set; } = null!;

        public DbSet<Drill> Drills { get; set; } = null!;

        public DbSet<Run> Runs { get; set; } = null;

        public DbSet<Level> Levels { get; set; } = null!;

        public DbSet<Skill> Skills { get; set; } = null;

        public DbSet<RunDisciplineDrill> RunDisciplineDrills { get; set; } = null;

        public DbSet<RunDiscipline> RunDisciplines { get; set; } = null;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Area>().ToTable("Area");
            modelBuilder.Entity<Run>().ToTable("Run");
            modelBuilder.Entity<Drill>().ToTable("Drill");
            modelBuilder.Entity<Level>().ToTable("Level");
            modelBuilder.Entity<Skill>().ToTable("Skill");
            modelBuilder.Entity<RunDisciplineDrill>().ToTable("RunDisciplineDrill");
            modelBuilder.Entity<RunDiscipline>().ToTable("RunDiscipline");

            modelBuilder.Entity<Drill>()
                .HasKey(d => d.Id);

            modelBuilder.Entity<Skill>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<Level>()
                .HasKey(l => l.Id);

            modelBuilder.Entity<RunDisciplineDrill>()
                .HasKey(rd => new { rd.RunDisciplineId, rd.DrillId });

            modelBuilder.Entity<Run>()
                .HasOne(r => r.Area)
                .WithMany(a => a.Runs)
                .HasForeignKey(r => r.AreaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Skill>()
              .HasOne(s => s.Level)
              .WithMany(l => l.Skills)
              .HasForeignKey(s => s.LevelId)
              .OnDelete(DeleteBehavior.Cascade);

            /* many to many */

            modelBuilder.Entity<RunDisciplineDrill>()
                 .HasOne(rdd => rdd.RunDiscipline)
                 .WithMany(rd => rd.RunDisciplineDrills)
                 .HasForeignKey(rdd => rdd.RunDisciplineId)
                 .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RunDisciplineDrill>()
                .HasOne(rdd => rdd.Drill)
                .WithMany(d => d.RunDisciplineDrills)
                .HasForeignKey(rdd => rdd.DrillId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}