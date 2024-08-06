using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OmniTracker.DataContext;
using OmniTracker.DataContext.Models;
using Entry = OmniTracker.DataContext.Models.Entry;

namespace OmniTracker;
public partial class TrackerViewModel(Tracker tracker, IServiceProvider serviceProvider) : ObservableObject
{
    public Tracker Tracker => tracker;

    public string Name => tracker.Name;

    public int Count => Tracker.Entries.Count;

    [RelayCommand]
    public async Task Increment()
    {
        try
        {
            var entry = new Entry
            {
                Tracker = Tracker,
            };
            var dataContext = serviceProvider.GetRequiredService<TrackerContext>();
            dataContext.Entries.Add(entry);
            await dataContext.SaveChangesAsync();
            OnPropertyChanged(nameof(Count));
            DecrementCommand.NotifyCanExecuteChanged();
        }
        catch (Exception) 
        {
            Toast.Make("Coundn't write Entry to DB");
        }
    }

    [RelayCommand(CanExecute = nameof(canDecrement))]
    public async Task Decrement()
    {
        try
        {
            var dataContext = serviceProvider.GetRequiredService<TrackerContext>();
            dataContext.Entries.Remove(Tracker.Entries.Last());
            await dataContext.SaveChangesAsync();
            OnPropertyChanged(nameof(Count));
            DecrementCommand.NotifyCanExecuteChanged();
        }
        catch (Exception)
        {
            Toast.Make("Coundn't write Entry to DB");
        }
    }

    private bool canDecrement() => Count > 0;

    [RelayCommand]
    public async Task Reset()
    {
        try
        {
            var dataContext = serviceProvider.GetRequiredService<TrackerContext>();
            dataContext.Entries.RemoveRange(Tracker.Entries);
            await dataContext.SaveChangesAsync();
            OnPropertyChanged(nameof(Count));
            DecrementCommand.NotifyCanExecuteChanged();
        }
        catch (Exception)
        {
            Toast.Make("Coundn't write Entry to DB");
        }
    }
}
