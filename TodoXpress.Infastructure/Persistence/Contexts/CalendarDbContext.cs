using Microsoft.EntityFrameworkCore;
using TodoXpress.Application.Contracts.Persistence;
using TodoXpress.Domain.Calendars;
using Color = TodoXpress.Domain.Common.Color;

namespace TodoXpress.Infastructure.Persistence.Contexts;

/// <summary>
/// The db context for the calendar domain.
/// </summary>
public sealed class CalendarDbContext : DbContext, IDbContext
{
    internal DbSet<Calendar> Calendars { get; set; }

    internal DbSet<CalendarEvent> Events { get; set; }

    internal DbSet<SerialEvent> SerialEvents { get; set; }

    internal DbSet<FileAttachment> FileAttachments { get; set; }

    internal DbSet<User> Users { get; set; }

    public CalendarDbContext(DbContextOptions<CalendarDbContext> options)
        : base(options)
    {
        if (this.Database.GetPendingMigrations().Any())
        {
            this.Database.Migrate();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Calendar");

        // Calendar
        modelBuilder.Entity<Calendar>()
            .HasMany(c => c.Events)
            .WithOne(e => e.Calendar)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Calendar>()
            .HasOne(c => c.Color);

        modelBuilder.Entity<Calendar>()
            .HasOne(c => c.Owner)
            .WithMany(o => o.Calendars)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Calendar>()
            .HasOne(c => c.Color);

        // Color
        modelBuilder.Entity<Color>();

        // Event
        modelBuilder.Entity<CalendarEvent>()
            .HasOne(e => e.SerialEvent)
            .WithMany(s => s.Events);

        modelBuilder.Entity<CalendarEvent>()
            .HasMany(e => e.FileAttachments)
            .WithOne(f => f.Event)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CalendarEvent>()
            .HasOne(e => e.Calendar);

        // Serial Event
        modelBuilder.Entity<SerialEvent>()
            .HasMany(s => s.Events)
            .WithOne(e => e.SerialEvent);

        // File Attachment
        modelBuilder.Entity<FileAttachment>()
            .HasOne(f => f.Event)
            .WithMany(e => e.FileAttachments);

        // User
        modelBuilder.Entity<User>()
            .HasMany(u => u.Calendars)
            .WithOne(c => c.Owner)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }
}
