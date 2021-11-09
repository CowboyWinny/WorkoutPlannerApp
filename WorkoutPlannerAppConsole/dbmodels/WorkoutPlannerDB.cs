using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WorkoutPlannerAppConsole
{
    public partial class WorkoutPlannerDB : DbContext
    {
        public WorkoutPlannerDB()
        {
        }

        public WorkoutPlannerDB(DbContextOptions<WorkoutPlannerDB> options)
            : base(options)
        {
        }

        public virtual DbSet<DayPlan> DayPlans { get; set; }
        public virtual DbSet<Excercise> Excercises { get; set; }
        public virtual DbSet<TagsForExcercise> TagsForExcercises { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string dbPath = @"H:\wpa.db";
                optionsBuilder.UseSqlite("Data Source=" + dbPath);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DayPlan>(entity =>
            {
                entity.ToTable("DayPlan");

                entity.HasIndex(e => e.ID, "IX_DayPlan_ID")
                    .IsUnique();

                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.ExcerciseID).HasColumnName("ExcerciseID");

                entity.Property(e => e.Repeats).HasDefaultValueSql("1");

                entity.Property(e => e.Rounds).HasDefaultValueSql("1");

                entity.HasOne(d => d.Excercise)
                    .WithMany(p => p.DayPlans)
                    .HasForeignKey(d => d.ExcerciseID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Excercise>(entity =>
            {
                entity.HasIndex(e => e.ID, "IX_Excercises_ID")
                    .IsUnique();

                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.TagsForExcercisesID).HasColumnName("TagsForExcercisesID");

                entity.HasOne(d => d.TagsForExcercises)
                    .WithMany(p => p.Excercises)
                    .HasForeignKey(d => d.TagsForExcercisesID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<TagsForExcercise>(entity =>
            {
                entity.HasIndex(e => e.ID, "IX_TagsForExcercises_ID")
                    .IsUnique();

                entity.Property(e => e.ID).HasColumnName("ID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
