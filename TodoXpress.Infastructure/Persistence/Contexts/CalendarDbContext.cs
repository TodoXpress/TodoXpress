using Microsoft.EntityFrameworkCore;
using TodoXpress.Application.Contracts.Persistence;
using TodoXpress.Domain.Calendars;

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
}
