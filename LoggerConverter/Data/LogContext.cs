using LoggerConverter.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace LoggerConverter.Data
{
    public class LogContext : DbContext
    {
        public LogContext(DbContextOptions<LogContext> options)
        : base(options)
        {
        }

        public DbSet<Log> Logs { get; set; }

        public DbSet<LogConverted> LogsConverted { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Log>(entity =>
            {
                entity.HasKey(s => s.Id);

                entity.Property(s => s.Path)
                    .HasMaxLength(200)
                    .IsRequired();

                entity.Property(s => s.IsConverted)
                    .HasDefaultValue(false)
                    .IsRequired();

                entity.Property(s => s.CreatedAt)
                    .HasDefaultValueSql("GETDATE()")
                    .IsRequired();

                entity.Property(s => s.UpdatedAt)
                    .HasDefaultValueSql("GETDATE()")
                    .IsRequired();
            });

            modelBuilder.Entity<LogConverted>(entity =>
            {
                entity.HasKey(s => s.Id);

                entity.Property(s => s.IdLog);

                entity.Property(s => s.CreatedAt)
                    .HasDefaultValueSql("GETDATE()")
                    .IsRequired();

                entity.Property(s => s.UpdatedAt)
                    .HasDefaultValueSql("GETDATE()")
                    .IsRequired();

                entity.Property(s => s.Path)
                    .HasMaxLength(200)
                    .IsRequired();

                entity.HasOne<Log>(s => s.Log)
                    .WithOne(ad => ad.LogConverted)
                    .HasForeignKey<LogConverted>(ad => ad.IdLog);
            });
        }
    }
}