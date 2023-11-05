using Microsoft.EntityFrameworkCore;
using TodoXpress.Application.Contracts.Persistence;
using TodoXpress.Domain.Calendars;
using Color = TodoXpress.Domain.Common.Color;

namespace TodoXpress.Infastructure.Persistence.Contexts;

public sealed class CalendarDbContext : DbContext, IDbContext
{
    public DbSet<Calendar> Calendars { get; set; }

    public DbSet<CalendarEvent> Events { get; set; }

    public DbSet<SerialEvent> SerialEvents { get; set; }

    public DbSet<FileAttachment> FileAttachments { get; set; }

    public DbSet<User> Users { get; set; }

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
