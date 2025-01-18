using Journey.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Journey.Infrastructure;

public class JourneyDbContext : DbContext
{
    public DbSet<Trip> Trips { get; set; }
    public DbSet<Activity> Activities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string? rdbFilePath = Environment.GetEnvironmentVariable("RDB_FILE_PATH");
        if (rdbFilePath is null)
		    throw new ArgumentNullException(rdbFilePath, "RDB_FILE_PATH environment variable not found");

		optionsBuilder.UseSqlite(rdbFilePath);
    }
}
