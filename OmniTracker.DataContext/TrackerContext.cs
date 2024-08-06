using Microsoft.EntityFrameworkCore;
using OmniTracker.DataContext.Models;

namespace OmniTracker.DataContext;

public class TrackerContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Tracker> Trackers { get; set; }
    public DbSet<Entry> Entries { get; set; }
}
