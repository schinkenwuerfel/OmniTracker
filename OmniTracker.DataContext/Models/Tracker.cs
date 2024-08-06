using UUIDNext;

namespace OmniTracker.DataContext.Models;
public class Tracker
{
    public Guid Id { get; internal set; } = Uuid.NewDatabaseFriendly(Database.SQLite);
    public string Name { get; set; } = "Tracker";

    public virtual IList<Entry> Entries { get; set; } = [];
}
