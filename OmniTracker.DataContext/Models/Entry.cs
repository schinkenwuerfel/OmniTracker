#nullable disable
using UUIDNext;

namespace OmniTracker.DataContext.Models;
public class Entry
{
    public Guid Id { get; internal set; } = Uuid.NewDatabaseFriendly(Database.SQLite);
    public DateTime Timestamp { get; set; } = DateTime.Now;
    public Guid TrackerId { get; internal set; }

    public Tracker Tracker { get; set; }
}
